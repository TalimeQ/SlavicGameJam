using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    [SerializeField] private GameObject spawnedFern = null;
    FernIgnore ignoredAxis;
    float ignoredSign;

    private SpawnerController spawningController;

    public void Init(SpawnerController currentSpawner, FernIgnore ignoredAxis, float ignoredSign)
    {
        ignoredSign = 1;
        ignoredSign *= Mathf.Sign(ignoredSign);
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
            GameObject createdFern = Instantiate(spawnedFern,transform.position,Quaternion.identity,transform);
            createdFern.GetComponent<AngryFern>()?.Init(ignoredAxis, ignoredSign,this);
        }
    }

    private void OnDisable()
    {
        spawningController.EnableSpawnerPoint(transform);
    }
}
