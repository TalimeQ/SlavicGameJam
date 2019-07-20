using System.Collections;
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
