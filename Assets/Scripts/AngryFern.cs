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

    public void Init()
    {
        isBeingDamaged = false;
        health = 0.0f;
    }

    public void OnWeaponDamaged(float Damage)
    {
        isBeingDamaged = true;
        health -= Damage * Time.deltaTime;
        transform.localScale -= growthRate * transform.localScale * Time.deltaTime;
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

    private void Update()
    {

        if(health >= maxHealth)
        {
            health = maxHealth;

        }
        else if(!isBeingDamaged)
        {
            transform.localScale += growthRate * transform.localScale * Time.deltaTime;
            health += growthRate * Time.deltaTime;
        }
    }


}
