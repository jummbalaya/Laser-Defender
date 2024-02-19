using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int healt = 50;
    [SerializeField] int score = 50;
    [SerializeField] ParticleSystem hitEffect;

    [SerializeField] bool applyCameraShake;

    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeeper scoreKeeper;

    private void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeeper>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealler damageDealer = other.GetComponent<DamageDealler>();

        if(damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            audioPlayer.PlayDamageTakenClip();
            ShakeCamera();
            damageDealer.Hit();
        }
    }

    private void ShakeCamera()
    {
        if(cameraShake !=null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }

    void TakeDamage(int damage)
    {
        healt -= damage;
        if (healt <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (!isPlayer)
        {
            scoreKeeper.ModifyScore(score);
        }
        Destroy(gameObject);
        Debug.Log(score + " => " + scoreKeeper.GetScore());
    }

    void PlayHitEffect()
    {
        ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
    }

    public int GetHealth()
    {
        return healt;
    }
}
