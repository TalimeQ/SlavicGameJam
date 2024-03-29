﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FernPooler : MonoBehaviour
{
    public int StartingPool = 10;
    public static FernPooler fernPool;

    List<GameObject> pool = new List<GameObject>();
    [SerializeField] GameObject FernPrefab;

    void Awake()
    {
        fernPool = this;
        GameObject G;
        for(int i = 0; i < StartingPool; i++)
        {
            G = Instantiate(FernPrefab);
            G.SetActive(false);
            pool.Add(G);
        }
    }

    public static int GetActiveFerns()
    {
        int activeFerns = 0;
        foreach(GameObject fern in fernPool.pool)
        {
            if (fern.activeInHierarchy) activeFerns++;
        }
        return activeFerns;
    }

    public static void DisableFerns()
    {
        foreach (GameObject fern in fernPool.pool)
        {
            fern.SetActive(false);
        }
    }

    public GameObject GetFern()
    {
        for(int i = 0; i < pool.Count; i++)
        {
            if(!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }
        GameObject G = Instantiate(FernPrefab);
        pool.Add(G);
        return G;
    }
    
}
