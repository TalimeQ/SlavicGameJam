using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//odpowiada za wlaczanie/wylaczanie drugiego gracza, podlaczanie padow
public class MovementManager : MonoBehaviour
{
    public bool HideSecondPlayerWhenDisabled = true;
    [SerializeField] GameObject SecondPlayer;
    public static MovementManager singleton;
    public enum MovementMode
    {
        single, mixed, pads //keyboard, keyboard + one pad, 2 pads
    }
    public MovementMode Mode { private set; get; }


    void Awake()
    {
        singleton = this;
        if (SecondPlayer == null)
            SecondPlayer = FindObjectOfType<MovementSecond>()?gameObject:null;
    }

    private void Start()
    {
        if (HideSecondPlayerWhenDisabled)
        {
            DisableSecondPlayer();
        }
    }

    void UpdateMovementMode()
    {
        string[] temp = Input.GetJoystickNames();

        int countpads = 0;
        if (temp.Length > 0)
        {
            for (int i = 0; i < temp.Length; ++i)
            {
                if (!string.IsNullOrEmpty(temp[i]))
                {
                    Debug.Log("nr = " + i);
                    countpads++;
                }
            }
        }
        if (countpads == 0)
        {
            if (SecondPlayer.activeInHierarchy)
                DisableSecondPlayer();
            Mode = MovementMode.single;
        }
        else if (countpads == 1)
        {
            Mode = MovementMode.mixed;
            if (!SecondPlayer.activeInHierarchy)
                ActivateSecondPlayer();
        }
        else
        {
            Mode = MovementMode.pads;
            if (!SecondPlayer.activeInHierarchy)
                ActivateSecondPlayer();
        }
        Debug.Log(countpads);
    }


    void ActivateSecondPlayer()
    {
        SecondPlayer.SetActive(true);
    }

    void DisableSecondPlayer()
    {
        if(HideSecondPlayerWhenDisabled)
            SecondPlayer.SetActive(false);
    }

    void Update()
    {
        UpdateMovementMode();


    }
}
