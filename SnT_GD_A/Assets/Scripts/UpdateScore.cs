using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpdateScore : MonoBehaviour
{
    public GameObject gameWonCanvas;
    public Text remainingEnemies;
    public Text gameWonText;
    public Text timeText; 

    private float enemiesLeft = 15;
    private float timeUsed = 0;
    private float min;
    private float sec;
    private float miliSec;
    private float mainMenuTimer = 5;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesLeft == 0){
            UpdateWinText(timeUsed);
            gameWonCanvas.SetActive(true);
            if (mainMenuTimer < 0){
                SceneManager.LoadScene("TitlePage");
            }
            else{
                mainMenuTimer -= Time.deltaTime;
            }
        }
        else{
            UpdateScoreText();
            timeUsed += Time.deltaTime;
        }
    }

    public void KilledEnemy(){
        enemiesLeft--;
        UpdateScoreText();
    }

    void UpdateScoreText(){
        remainingEnemies.text = string.Format("Enemies left:\n{0:0}", enemiesLeft);
    }

    void UpdateWinText(float timeUsed){
        min = Mathf.FloorToInt(timeUsed / 60);
        sec = Mathf.FloorToInt(timeUsed % 60);
        miliSec = (timeUsed - Mathf.Floor(timeUsed))*1000;
        gameWonText.text = string.Format("Game Won!\n Your time was:\n{0:00}:{1:00}:{2:000}", min, sec, miliSec);
    }
    
}
