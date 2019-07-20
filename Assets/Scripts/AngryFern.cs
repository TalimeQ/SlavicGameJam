using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerCombat;


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
    private bool isDead = false;
    private bool hasSpawned = false;
    private int[] choicesArr = {-1,1};
    private SpawnPoint spawningPoint;

    public void Init(FernIgnore ignoredAxis, float ignoredSign, SpawnPoint owningSpawningPoint)
    {
        hasSpawned = false;
        isBeingDamaged = false;
        isDead = false;
        spawningPoint = owningSpawningPoint;
        ignoredAxisSign = 1;
        ignoredAxisSign *= (int) Mathf.Sign(ignoredSign);
        ignoredSpawnAxis = ignoredAxis;
        health = 0.1f;
    }

    public void OnWeaponDamaged(float Damage, PlayerWeapon playerWeaponRef)
    {
        isBeingDamaged = true;
        health -= Damage;
        SetScale();
        if (health <= 0  && !isDead)
        {
            isDead = true;
            playerWeaponRef.RemoveFern(this);
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
            GameObject spawnedFern = FernPooler.fernPool.GetFern();
            spawnedFern.transform.position = duplicateSpawnPos;
            spawnedFern.transform.parent = transform.parent.transform;
            spawnedFern.GetComponent<AngryFern>()?.Init(ignoredSpawnAxis, ignoredAxisSign, spawningPoint);
            spawnedFern.GetComponent<AngryFern>()?.CheckBounds();
            spawnedFern.transform.localScale = new Vector3(1, 1, 1);
            spawnedFern.name = "Fern";
            spawnedFern.SetActive(true);
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

    private void SetScale()
    {
        float scale = maxHealth / health;
        transform.localScale = maxScale / scale;
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
        spawningPoint.SignalizeInactivity();
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
            health += growthRate * Time.deltaTime;
            SetScale();
        }
    }

}
