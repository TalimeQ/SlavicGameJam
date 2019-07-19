using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
    {
    public GameObject plantObject;
    public GameObject SpawnPointParent;
    private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField]
    private float regularTimeBetweenSpawns;
    [SerializeField]
    private float longTimeBetweenSpawns;
    private Dictionary<Transform, bool> occupiedPositions = new Dictionary<Transform, bool>();

    private int nextSpawnPointID;


    private void Start()
    {
        foreach (Transform child in SpawnPointParent.transform)
        {
            occupiedPositions[child] = false;
        }

        StartCoroutine(SpawnPlant());

    }

    IEnumerator SpawnPlant()
    {
        while (true){

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

            Transform spawnTransform = spawnPoints[nextSpawnPointID];

            Instantiate(plantObject, spawnTransform.position, Quaternion.identity);

            occupiedPositions[spawnTransform] = true;

            spawnPoints.Clear();

            yield return new WaitForSeconds(regularTimeBetweenSpawns);

        }
        }
    }

    public void PickRandomPosition()
    {
            nextSpawnPointID = Random.Range(0, spawnPoints.Count);
    }

    public void EnableSpawnerPoint(Transform spawnTransform)
    {
        occupiedPositions[spawnTransform] = false;
    }

}
