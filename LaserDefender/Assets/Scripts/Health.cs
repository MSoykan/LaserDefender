using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem hitEffect;

    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;

    AudioPlayer audioPlayer;

    private void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        if(damageDealer != null)
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
        health -= damageTaken;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void PlayHitEffect()
    {
        if(hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            instance.transform.parent = transform;
            Destroy(instance.gameObject, instance.main.duration+instance.main.startLifetime.constantMax);
        }
    }

    void ShakeCamera()
    {
        if(cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }
}
