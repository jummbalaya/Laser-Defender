using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class AsteroidsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroids;
    [SerializeField] private int arraySize = 7;
    [SerializeField] private float asteroidsSpawned = 10;
    [SerializeField] private float spawnInterval = 10;
    [SerializeField] private float timer = 0;

    private float delayBbetweenSpawns = 0.60f;
    
    int index;
    float spawnXRange = 4.5f;
    float spawnYRange = 11.0f;
    float spawnX, spawnY, spawnZ;
    Vector3 SpawnVector;


    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;

            int numOfAsteroids = (int)UnityEngine.Random.Range(asteroidsSpawned, asteroidsSpawned * 0.5f);
            StartCoroutine(SpawnAsteroids(numOfAsteroids));
        }
    }

    IEnumerator SpawnAsteroids(int numOfAsteroids)
    {
        for (int i = 0; i <= numOfAsteroids; i++)
        {
            index = UnityEngine.Random.Range(0, arraySize);
            spawnX = UnityEngine.Random.Range(-spawnXRange, spawnXRange);
            spawnY = UnityEngine.Random.Range(spawnYRange, spawnYRange * 1.5f);
            SpawnVector = new Vector3(spawnX, spawnY, spawnZ);
            Instantiate(asteroids[index], SpawnVector, Quaternion.identity);

            yield return new WaitForSeconds(delayBbetweenSpawns);
        }
    }
}
