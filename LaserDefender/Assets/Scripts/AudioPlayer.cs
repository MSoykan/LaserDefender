using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField][Range(0f, 1f)] float shootingVolume = 1.0f;

    [Header("Explosion")]
    [SerializeField] AudioClip explosionClip;
    [SerializeField][Range(0f, 1f)] float explosionVolume = 1.0f;

    static  AudioPlayer instance; // Singleton on large projects is an absolute nightmare.

    private void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Dont destroy the gameobjet upon loading any other scene
        }
    }

    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }
    public void PlayExplosionClip()
    {
        PlayClip(explosionClip, explosionVolume);
    }

    public void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip,
                                        Camera.main.transform.position,
                                        volume);
        }
    }

}
