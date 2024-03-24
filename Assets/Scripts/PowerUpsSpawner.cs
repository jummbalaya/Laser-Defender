using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] powerUpPrefabs;
    [SerializeField] private float spawnInterval = 5.0f;

    private float spawnPosZ = 0.0f;
    private float spawnPosY = 10.5f;
    private float spawnPosX;
    private Vector3 spawnPosition;
    

    private float spawnTimer;

    private void Start()
    {
        spawnTimer = spawnInterval;
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnPowerUp();
            spawnTimer = spawnInterval;
        }
    }

    private void SpawnPowerUp()
    {
        int prefabIndex = UnityEngine.Random.Range(0, powerUpPrefabs.Length);
        GameObject powerUpPrefab = powerUpPrefabs[prefabIndex];
        int xSpawnRange = 4;

        spawnPosX = UnityEngine.Random.Range(-xSpawnRange, xSpawnRange);
        spawnPosition = new Vector3(spawnPosX, spawnPosY, spawnPosZ);

        Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);
    }
}
