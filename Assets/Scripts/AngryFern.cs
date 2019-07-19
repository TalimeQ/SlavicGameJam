using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum FernIgnore
{
    ignoreX,
    ignoreY
}
public class AngryFern : MonoBehaviour
{
    [SerializeField] private float growthRate;
    [SerializeField] private float maxHealth;
    [SerializeField] private float minimalRange;
    [SerializeField] private float maximalRange;
    [SerializeField] private Vector3 maxScale;
    [SerializeField] private Vector2 minBounds;
    [SerializeField] private Vector2 maxBounds;
    [SerializeField] private FernIgnore ignoredSpawnAxis;
    [SerializeField] private int ignoredAxisSign = 1;
  
    private float health = 0.0f;
    private bool isBeingDamaged = false;
    private bool hasSpawned = false;
    private int[] choicesArr = {-1,1};
    private SpawnPoint spawningPoint;

    public void Init(FernIgnore ignoredAxis, float ignoredSign, SpawnPoint owningSpawningPoint)
    {
        hasSpawned = false;
        isBeingDamaged = false;
        spawningPoint = owningSpawningPoint;
        ignoredAxisSign = 1;
        ignoredAxisSign *= (int) Mathf.Sign(ignoredSign);
        ignoredSpawnAxis = ignoredAxis;
        health = 0.0f;
    }

    public void OnWeaponDamaged(float Damage)
    {
        isBeingDamaged = true;
        health -= Damage * Time.deltaTime;
        transform.localScale -= Damage * transform.localScale * Time.deltaTime;
        if (health <= 0)
        {
            health = 0;
            gameObject.SetActive(false);
        }
    }

    public void FinishedDamaging()
    {
        isBeingDamaged = false;
    }

    private void Duplicate()
    {
        if(hasSpawned)
        {
            return;
        }
        else
        {
            hasSpawned = true;
            Vector3 duplicateSpawnPos = GetSpawnOffset();
            GameObject spawnedObject = Instantiate(this.gameObject, duplicateSpawnPos, Quaternion.identity, transform.parent.transform);
            if (spawnedObject == null) Debug.Log("null");
            spawnedObject.GetComponent<AngryFern>()?.Init(ignoredSpawnAxis, ignoredAxisSign, spawningPoint);
            spawnedObject.GetComponent<AngryFern>()?.CheckBounds();
            spawnedObject.transform.localScale = new Vector3(1, 1, 1);
            spawnedObject.name = "Fern";
        }
    }

    private Vector3 GetSpawnOffset()
    {
        Vector3 minimalSpawnValues, maximalSpawnValues;
        GetSpawnRanges(out minimalSpawnValues,out maximalSpawnValues);
        float xOffset = Random.Range(minimalRange, maximalRange) * choicesArr[Random.Range(0, choicesArr.Length)]; 
        float yOffset = Random.Range(minimalRange, maximalRange) * choicesArr[Random.Range(0, choicesArr.Length)];
        xOffset = Mathf.Clamp(xOffset, minimalSpawnValues.x, maximalSpawnValues.x);
        yOffset = Mathf.Clamp(yOffset, minimalSpawnValues.y, maximalSpawnValues.y);
        Vector3 duplicateSpawnPos = transform.position;
        Vector3 spawnOffset = new Vector3(xOffset, yOffset, 0);
        duplicateSpawnPos += spawnOffset;
        return duplicateSpawnPos;
    }

    private void GetSpawnRanges(out Vector3 minimalRange, out Vector3 maximalRange)
    {
        if(ignoredSpawnAxis == FernIgnore.ignoreX)
        {
            if(ignoredAxisSign < 0)
            {
                minimalRange = new Vector3(0.0f, -1.0f);
                maximalRange = new Vector3(1.0f, 1.0f);
            }
            else
            {
                minimalRange = new Vector3(-1.0f, -1.0f);
                maximalRange = new Vector3(0.0f, 1.0f);
            }
        }
        else if (ignoredSpawnAxis == FernIgnore.ignoreY)
        {
            if (ignoredAxisSign < 0)
            {
                minimalRange = new Vector3(-1.0f, 0.0f);
                maximalRange = new Vector3(1.0f, 1.0f);
            }
            else
            {
                minimalRange = new Vector3(-1.0f, -1.0f);
                maximalRange = new Vector3(1.0f, 0.0f);
            }
        }
        else
        {
            minimalRange = new Vector3(0.0f, 0.0f);
            maximalRange = new Vector3(0.0f, 0.0f);
        }
    }

    private void CheckBounds()
    {
        if(transform.position.x > maxBounds.x || transform.position.x < minBounds.x)
        {
            gameObject.SetActive(false);
            SignalizeSpawner();
        }
        if(transform.position.y > maxBounds.y || transform.position.y < minBounds.y)
        {
            gameObject.SetActive(false);
            SignalizeSpawner();
        }
    }

    private void SignalizeSpawner()
    {
        spawningPoint.OnBoundsExtended();
    }

    private void Update()
    {

        if(health >= maxHealth)
        {
            health = maxHealth;
            Duplicate();
        }
        else if(!isBeingDamaged)
        {
            transform.localScale += growthRate * transform.localScale * Time.deltaTime;
            health += growthRate * Time.deltaTime;
        }
    }

}
