using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnPoint : MonoBehaviour
{

    [SerializeField] private GameObject spawnedFern = null;
    [SerializeField] private float timeToDeactivation = 7.0f;
    FernIgnore ignoredAxis;
    float ignoredSign;

    private SpawnerController spawningController;

    public void Init(SpawnerController currentSpawner, FernIgnore ignoredAxis, float ignoredSign)
    {
        Debug.Log(ignoredAxis);
        this.ignoredSign = 1;
        this.ignoredSign *= Mathf.Sign(ignoredSign);
        Debug.Log(this.ignoredSign);
        this.ignoredAxis = ignoredAxis;
        Debug.Log(this.ignoredAxis);
        spawningController = currentSpawner;
        SpawnFern();
        StartCoroutine(DisableSpawnPoint());

    }

    public void SignalizeInactivity()
    {
        SpawnFern();
    }

    private void SpawnFern()
    {
        GameObject spawnedFern = FernPooler.fernPool.GetFern();
        spawnedFern.transform.position = transform.position;
        spawnedFern.transform.parent = transform.parent.transform;
        spawnedFern.GetComponent<AngryFern>()?.Init(ignoredAxis, ignoredSign, this);
        spawnedFern.transform.localScale = new Vector3(1, 1, 1);
        spawnedFern.SetActive(true);
    }

    private void OnDisable()
    {
        spawningController.EnableSpawnerPoint(transform);
    }

    IEnumerator DisableSpawnPoint()
    {
        yield return new WaitForSeconds(timeToDeactivation);
        gameObject.SetActive(false);
        StopCoroutine(DisableSpawnPoint());
    }
}
