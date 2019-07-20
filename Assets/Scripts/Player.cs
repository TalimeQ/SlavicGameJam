using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float Hp;
    [SerializeField] private float MaxHp = 100;
    [SerializeField] int FernDmg = 5;


    void Awake()
    {
        Hp = MaxHp;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        AngryFern fernToCut = collision.GetComponent<AngryFern>();
        if (fernToCut != null)
        {
            Hp -= Time.deltaTime * FernDmg;
        }
    }
}
