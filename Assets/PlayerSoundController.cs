using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{ 
    [SerializeField] private string HorizontalAxisString;
    [SerializeField] private string VerticalAxisString;
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton(HorizontalAxisString) || Input.GetButton(VerticalAxisString))
        {
            if (!audioSource.isPlaying)
            {
            audioSource.Play();
            }
        }
    }
}
