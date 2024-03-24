using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 1.0f;

    [Header("Damage")]
    [SerializeField] AudioClip damageClip;
    [SerializeField][Range(0f, 1f)] float damageVolume = 1.0f;

    [Header("Player Shield")]
    [SerializeField] AudioClip shieldClip;
    [SerializeField][Range(0f, 1f)] float shieldVolume = 0.8f;

    static AudioPlayer instance;

    void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayDamageTakenClip()
    {
        PlayClip(damageClip, damageVolume);
    }

    public void PlayShieldClip()
    {
        PlayClip(shieldClip, shieldVolume);
    }

    void PlayClip(AudioClip clip, float volume)
    {
        Vector3 cameraPos = Camera.main.transform.position;
        AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
    }
}
