using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    [SerializeField] private GameObject spawnedFern = null;
    FernIgnore ignoredAxis;
    float ignoredSign;

    private SpawnerController spawningController; 

    public void Init(SpawnerController currentSpawner)
    {
        spawningController = currentSpawner;
        SpawnFern();
    }

    public void OnBoundsExtended()
    {
        SpawnFern();
    }

    private void SpawnFern()
    {
        if(spawnedFern != null)
        {
            GameObject spawnedFern = Instantiate(gameObject, this.transform);
            spawnedFern.GetComponent<AngryFern>()?.Init()
        }
    }

    private void OnDisable()
    {
        
    }
}
