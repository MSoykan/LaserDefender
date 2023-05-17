using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] int shipScore = 50;
    [SerializeField] ParticleSystem hitEffect;

    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;

    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());//Take damage
            ShakeCamera();
            audioPlayer.PlayExplosionClip();
            PlayHitEffect();
            damageDealer.Hit();//tell damage dealer it hit something

        }


    }
    void TakeDamage(int damageTaken)
    {
        Debug.Log("My score is " + scoreKeeper.GetScore());
        health -= damageTaken;
        if (transform.tag == "Player")
        {
            scoreKeeper.modifyScore(-10);
        }
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (transform.tag == "Enemy")
        {
            scoreKeeper.modifyScore(shipScore);
        }
        else
        {
            scoreKeeper.resetScore();
        }
        Destroy(gameObject);
    }

    public int GetHealth()
    {
        return health;
    }

    void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            instance.transform.parent = transform;
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }
}
