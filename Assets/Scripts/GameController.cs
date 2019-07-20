using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private float maxCorruption;
    [SerializeField] private float corruptionPerFern;
    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private Image filledBar;
    [SerializeField] private SpawnerController spawnController;
    [SerializeField] TextMeshProUGUI TimeSurvivedPanel;

    [SerializeField] float TimeFromGameStart = 0;

    private float corruptionLevel;

    private void Start()
    {
        TimeFromGameStart = 0;
        corruptionLevel = 0;
        StartCoroutine(UpdateCorruption());
    }

    private void Update()
    {
        if (!GameOverScreen.activeSelf)
            TimeFromGameStart += Time.deltaTime;

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
        TimeSurvivedPanel.text = (Math.Round(TimeFromGameStart, 2)).ToString() + "s";
        StopAllCoroutines();
        spawnController.DisableSpawning();
        GameOverScreen.SetActive(true);
        FernPooler.DisableFerns();
    }
}
