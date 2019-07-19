using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
public GameObject plantObject;



[SerializeField]
private float timeBetweenSpawns;

    IEnumerator SpawnPlant()
    {
        yield return new WaitForSeconds(timeBetweenSpawns);
        Instantiate(plantObject, transform.position, Quaternion.identity);
        
        

    }

    public void BeginSpawningPlants()
    {
        while (true)
        StartCoroutine(SpawnPlant());
    }

    public void StopSpawningPlants()
    {
        StopCoroutine(SpawnPlant());
    }
}
