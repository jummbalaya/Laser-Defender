using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int maxHealth = 75;
    [SerializeField] int healt;
    [SerializeField] int score = 50;
    [SerializeField] ParticleSystem hitEffect;

    [SerializeField] bool applyCameraShake;

    [SerializeField] int powerUpHealthAmount = 10;

    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeeper scoreKeeper;
    LevelManager levelManager;
    PlayerController playerController;
    PlayerCloner playerCloner;
    Shooter shooter;

    private void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeeper>();
        levelManager = FindObjectOfType<LevelManager>();
        playerController = FindObjectOfType<PlayerController>();
        playerCloner = FindAnyObjectByType<PlayerCloner>();
        shooter = GetComponent<Shooter>();
    }

    private void Start()
    {
        healt = maxHealth;
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

        if (other.CompareTag("PowerUpHealth"))
        {
            if(isPlayer && healt < maxHealth)
            {
                healt += powerUpHealthAmount;
                healt = Mathf.Clamp(healt, 0, maxHealth);
                Destroy(other.gameObject);
            }
        }

        if (other.CompareTag("PowerUpShield"))
        {
            if (isPlayer)
            {
                playerController.HasShieldUp = true;
                playerController.shieldHP = 40.0f;
                Destroy(other.gameObject);
            }
        }

        if (other.CompareTag("PowerUpFire"))
        {
            if (isPlayer)
            {
                shooter.firingBonus = true;
                Destroy(other.gameObject);
            }
        }

        if (other.CompareTag("Asteroid"))
        {
            if (isPlayer)
            {
                PlayHitEffect();
                audioPlayer.PlayDamageTakenClip();
                ShakeCamera();
                Die();
            }

            PlayHitEffect();
            audioPlayer.PlayDamageTakenClip();
            Destroy(gameObject);
            Destroy(other.gameObject);
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
        if (playerController.HasShieldUp)
        {
            playerController.shieldHP -= damage;
            audioPlayer.PlayShieldClip();
            if (playerController.shieldHP <= 0)
            {
                playerController.ShieldSpriteActive(-1);
                playerController.HasShieldUp = false;
            }
        }
        else
        {
            healt -= damage;
            if (healt <= 0)
            {
                Die();
            }
        }
        
    }

    private void Die()
    {
        if (!isPlayer)
        {
            scoreKeeper.ModifyScore(score);
        }
        else
        {
            levelManager.LoadGameOver();
        }
        Destroy(gameObject);
        //Debug.Log(score + " => " + scoreKeeper.GetScore());

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

    public int GetShield()
    {
        return (int)playerController.shieldHP;
    }
}
