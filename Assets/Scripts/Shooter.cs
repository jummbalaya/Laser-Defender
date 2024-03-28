using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10.0f;
    [SerializeField] float projectileLifeTime = 5.0f;
    [SerializeField] float baseFiringRate = 0.3f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0.0f;
    [SerializeField] float minFiringRate = 0.1f;

    [HideInInspector] public bool isFiring;
    
    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();

    }


    private void Start()
    {
        if(useAI)
        {
            isFiring = true;
        }
    }

    private void Update()
    {
        Fire();
    }

    public void Fire()
    {
        if(isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if(!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while(true)
        {
            GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            
            if (rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }

            Destroy(instance, projectileLifeTime);

            float timeToNextProjectile = UnityEngine.Random.Range(baseFiringRate - firingRateVariance, baseFiringRate + firingRateVariance);
            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minFiringRate, float.MaxValue);

            audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(timeToNextProjectile);
        }
    }
}
