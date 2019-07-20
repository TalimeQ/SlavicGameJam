using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private float maxCorruption;
    [SerializeField] private float corruptionPerFern;
    [SerializeField] private Image filledBar;
    private float corruptionLevel;

    private void Start()
    {
        corruptionLevel = 0;
        StartCoroutine(UpdateCorruption());
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.R))
        {
            Restart();
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator UpdateCorruption()
    {
        while(true)
        { 
            int activeFerns = FernPooler.GetActiveFerns();
            corruptionLevel += activeFerns * corruptionPerFern;
            filledBar.fillAmount = corruptionLevel / maxCorruption;
            if(corruptionLevel > maxCorruption)
            {
                PlayerLost();
            }
            yield return new WaitForSeconds(1);
        }

    }
    
    private void PlayerLost()
    {
        Debug.Log("Lost!");
        StopAllCoroutines();
    }
}
