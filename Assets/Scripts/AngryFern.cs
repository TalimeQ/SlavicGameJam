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
    [SerializeField] private FernIgnore ignoredSpawnAxis;
    [SerializeField] private int ignoredAxisSign = 1;

    private float health = 0.0f;
    private bool isBeingDamaged = false;
    private bool hasSpawned = false;
    private int[] choicesArr = {-1,1};

    public void Init(FernIgnore ignoredAxis, float ignoredSign)
    {
        hasSpawned = false;
        isBeingDamaged = false;
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
            GameObject spawnedObject = Instantiate<GameObject>(gameObject, duplicateSpawnPos, Quaternion.identity, transform.parent.transform);
            spawnedObject.GetComponent<AngryFern>()?.Init(ignoredSpawnAxis, ignoredAxisSign);
            spawnedObject.transform.localScale = new Vector3(1, 1, 1);
            spawnedObject.name = "Fern";
        }
    }

    private Vector3 GetSpawnOffset()
    {
        Vector3 minimalSpawnValues, maximalSpawnValues;
        GetSpawnRanges(out minimalSpawnValues,out maximalSpawnValues);
        Vector3 duplicateSpawnPos = transform.position;
        float xOffset = Random.Range(minimalRange, maximalRange) * choicesArr[Random.Range(0, choicesArr.Length)]; 
        float yOffset = Random.Range(minimalRange, maximalRange) * choicesArr[Random.Range(0, choicesArr.Length)];
        xOffset = Mathf.Clamp(xOffset, minimalSpawnValues.x, maximalSpawnValues.x);
        yOffset = Mathf.Clamp(yOffset, minimalSpawnValues.y, maximalSpawnValues.y);
        Vector3 spawnOffset = new Vector3(xOffset, yOffset, 0);
        Debug.Log(spawnOffset);
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
                Debug.Log("Ignored minus X");
            }
            else
            {
                minimalRange = new Vector3(-1.0f, -1.0f);
                maximalRange = new Vector3(0.0f, 1.0f);
                Debug.Log("Ignored plus X");
            }
        }
        else if (ignoredSpawnAxis == FernIgnore.ignoreY)
        {
            if (ignoredAxisSign < 0)
            {
                minimalRange = new Vector3(-1.0f, 0.0f);
                maximalRange = new Vector3(1.0f, 1.0f);
                Debug.Log("Ignored minus Y");
            }
            else
            {
                minimalRange = new Vector3(-1.0f, -1.0f);
                maximalRange = new Vector3(1.0f, 0.0f);
                Debug.Log("Ignored plus Y");
            }
        }
        else
        {
            minimalRange = new Vector3(0.0f, 0.0f);
            maximalRange = new Vector3(0.0f, 0.0f);
        }
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
