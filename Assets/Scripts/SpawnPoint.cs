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
}
