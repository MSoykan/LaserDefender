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
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] float shotTimeVariance = 0.3f;
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
        while (true)
        {
            GameObject instance = Instantiate(projectilePrefab,
                                              transform.position,
                                              Quaternion.identity);

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
            Destroy(instance, projectileLifeTime);
            float timeToNextProjectile = Random.Range(timeBetweenShots - shotTimeVariance,
                                                timeBetweenShots + shotTimeVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);

            audioPlayer.PlayShootingClip();
            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }
}
