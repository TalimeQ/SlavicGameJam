﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
    {
    [SerializeField] private GameObject plantObject;
    [SerializeField] private float regularTimeBetweenSpawns;
    [SerializeField] private float longTimeBetweenSpawns;
    [SerializeField] private Vector2 minBounds;
    [SerializeField] private Vector2 maxBounds;
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private List<GameObject> spawners = new List<GameObject>();

    private Dictionary<Transform, bool> occupiedPositions = new Dictionary<Transform, bool>();
    private int nextSpawnPointID;
    private bool shouldSpawn = true;
    Coroutine lastRoutine = null;


    public void PickRandomPosition()
    {
        nextSpawnPointID = UnityEngine.Random.Range(0, spawnPoints.Count);
    }

    public void EnableSpawnerPoint(Transform spawnTransform)
    {
        occupiedPositions[spawnTransform] = false;
    }

    public void DisableSpawning()
    {
        StopCoroutine(lastRoutine);
        Deinit();
    }

    private void Deinit()
    {
        foreach (GameObject spawn in spawners)
        {
            spawn.SetActive(false);
        }
    }

    private void Start()
    {
        foreach (Transform spawnPoint  in spawnPoints)
        {
            occupiedPositions[spawnPoint] = false;
        }
        lastRoutine =  StartCoroutine(SpawnPlant());
    }

    private void CreateSpawner(Transform spawnTransform)
    {
        FernIgnore ignoredAxis = FernIgnore.ignoreX;
        float ignoredSign = 1;
        CompareSpawnedPosition(spawnTransform, out ignoredAxis, out ignoredSign);
        GameObject currentSpawner = Instantiate(plantObject, spawnTransform.position, Quaternion.identity, transform);
        spawners.Add(currentSpawner);
        currentSpawner.GetComponent<SpawnPoint>()?.Init(this, ignoredAxis, ignoredSign);
    
    }

    private void CompareSpawnedPosition(Transform checkedTransform, out FernIgnore ignoredAxis, out float ignoredSign)
    {
        Vector3 positionToCheck = checkedTransform.position;
        if(positionToCheck.x > maxBounds.x)
        {
            ignoredAxis = FernIgnore.ignoreX;
            ignoredSign = 1;
            return;
        }
        if(positionToCheck.x < minBounds.x)
        {
            ignoredAxis = FernIgnore.ignoreX;
            ignoredSign = -1;
            return;
        }
        if(positionToCheck.y > maxBounds.y)
        {
            ignoredAxis = FernIgnore.ignoreY;
            ignoredSign = 1;
            return;
        }
        if(positionToCheck.y < minBounds.y)
        {
            ignoredAxis = FernIgnore.ignoreY;
            ignoredSign = -1;
            return;
        }
        else
        {
            Debug.Log("Compare spawned pos :: error");
            ignoredAxis = FernIgnore.ignoreX;
            ignoredSign = 1;
        }
    }

    IEnumerator SpawnPlant()
    {
        while (true)
        {
            if (!occupiedPositions.ContainsValue(false))
            {
                yield return new WaitForSeconds(longTimeBetweenSpawns);
            } 
            else
            {
                foreach(KeyValuePair<Transform, bool> entry in occupiedPositions)
                {
                    if (entry.Value == false)
                    {
                        spawnPoints.Add(entry.Key);
                    }
                }
                    PickRandomPosition();
                    Transform spawnTransform = spawnPoints[nextSpawnPointID];
                    CreateSpawner(spawnTransform);
                    occupiedPositions[spawnTransform] = true;
                    spawnPoints.Clear();
                    yield return new WaitForSeconds(regularTimeBetweenSpawns);
            }
        }
    }
}
