using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 5f;
    //[SerializeField] float firingRate = 0.1f;

    [Header("AI")]
    [SerializeField] float timeBetweenShots = 2f;
    [SerializeField] float shotTimeVariance = 0.01f;
    [SerializeField] float minimumFiringRate = 0.1f;
    [SerializeField] bool useAI;


    [HideInInspector] public bool isFiring;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        GameObject instance = null;
        while (true)
        {
            if (!useAI)
            {
                Debug.Log("Trying to fetch from pool");
                instance = LaserObjectPool.instance.GetPooledObject();
                if (instance != null)
                {
                    Debug.Log("Fetched from pool");

                    instance.transform.position = transform.position;
                    instance.SetActive(true);


                    //Instantiate(projectilePrefab,
                    //                              transform.position,
                    //                              Quaternion.identity);

                    Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        //if (useAI)
                        //{
                        //    rb.velocity = (-(transform.up * projectileSPeed));
                        //}
                        //else
                        //{
                        //}
                        rb.velocity = ((transform.up * projectileSpeed));
                    }
                    //Destroy(instance, projectileLifeTime);
                    float timeToNextProjectile = Random.Range(timeBetweenShots - shotTimeVariance,
                                                        timeBetweenShots + shotTimeVariance);
                    timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);
                    Debug.Log("Time to next projectile is :" + timeToNextProjectile);
                    audioPlayer.PlayShootingClip();
                    yield return new WaitForSeconds(timeToNextProjectile);
                    //instance.SetActive(false);
                    Debug.Log("Setting active laser to false");
                }

                else
                {
                    Debug.Log("The return value of GetObjectFromPool was null.");
                }

            }
            else
            {
                instance = Instantiate(projectilePrefab,
                                              transform.position,
                                              Quaternion.identity);
                Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = ((transform.up * projectileSpeed));

                    //rb.velocity = ((transform.up * projectileSpeed));
                }
                Destroy(instance, projectileLifeTime);
                float timeToNextProjectile = Random.Range(timeBetweenShots - shotTimeVariance,
                                                    timeBetweenShots + shotTimeVariance);
                timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);
                audioPlayer.PlayShootingClip();
                yield return new WaitForSeconds(timeToNextProjectile);

            }
        }

    }
}
