using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
    {
    public GameObject plantObject;
    public GameObject SpawnPointParent;
    private List<Vector3> spawnPoints = new List<Vector3>();
    [SerializeField]
    private float timeBetweenSpawns;

    int nextSpawnPointID;


    void Start()
    {
        foreach (Transform child in SpawnPointParent.transform)
        {
            spawnPoints.Add(child.position);
        } 
    }

    IEnumerator SpawnPlant(Vector3 spawnTransform)
    {
        yield return new WaitForSeconds(timeBetweenSpawns);
        Instantiate(plantObject, spawnTransform, Quaternion.identity);
    }

    public void BeginSpawningPlants()
    {
            nextSpawnPointID = Random.Range(0, spawnPoints.Count);
            StartCoroutine(SpawnPlant(spawnPoints[nextSpawnPointID]));
    }

    private void GenerateRandomSpawnID()
    {
        
    }
}
