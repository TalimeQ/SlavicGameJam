using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryFern : MonoBehaviour
{
    [SerializeField] private float growthRate;
    [SerializeField] private float maxHealth;
    [SerializeField] private Vector3 maxScale;

    private float health = 0.0f;
    private bool isBeingDamaged = false;
    private bool hasSpawned = false;
    int[] choicesArr = {-1,1};

    public void Init()
    {
        hasSpawned = false;
        isBeingDamaged = false;
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
            spawnedObject.transform.localScale = new Vector3(1, 1, 1);
            spawnedObject.name = "Fern";
        }
    }

    private Vector3 GetSpawnOffset()
    {
        Vector3 duplicateSpawnPos = transform.position;
        float xOffset = Random.Range(0.5f, 1) * choicesArr[Random.Range(0, choicesArr.Length)];
        float yOffset = Random.Range(0.5f, 1) * choicesArr[Random.Range(0, choicesArr.Length)];
        Vector3 spawnOffset = new Vector3(xOffset, yOffset, 0);
        duplicateSpawnPos += spawnOffset;
        return duplicateSpawnPos;
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
