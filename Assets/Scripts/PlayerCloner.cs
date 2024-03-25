using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCloner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject clone;

    Vector3[] spawnPositions = new Vector3[3];
    private float cloneDuration = 20f;
    private float cloneTimer;
    private float cloneSpawnCord = 0.5f;

    void Start()
    {
        cloneTimer = cloneDuration;

        spawnPositions[0] = new Vector3(0, 5, 0);
        spawnPositions[1] = new Vector3(-cloneSpawnCord, cloneSpawnCord, 0);
        spawnPositions[2] = new Vector3(cloneSpawnCord, cloneSpawnCord, 0);
    }
    

    void Update()
    {
        cloneTimer -= Time.deltaTime;
        if (cloneTimer <= 0f)
        {
            //DestroyClone();
            return;
        }

        //MimicPlayerActions();
    }

    void MimicPlayerActions()
    {
        clone.transform.position = playerPrefab.transform.position;
        clone.transform.rotation = playerPrefab.transform.rotation;
    }

    void CreateClone()
    {
        int index = spawnPositions.Length - 1;

        for (int i = 0; i <= index; i++)
        {
            Instantiate(clone, playerPrefab.transform.position + spawnPositions[i], playerPrefab.transform.rotation);
        }
    }

    void DestroyClone()
    {
        Destroy(clone);
    }

    public void TriggerCloneCreation()
    {
        CreateClone();
    }
}
