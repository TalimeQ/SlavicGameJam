using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    static bool musicManagerExists = false;
   

    void Awake()
    {
        if (!musicManagerExists)
        {
        DontDestroyOnLoad(gameObject);
        musicManagerExists = true;
        GetComponent<AudioSource>().Play();
        
        }
    }
}
