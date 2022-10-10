using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpdateScore : MonoBehaviour
{
    // GameObject references
    public GameObject gameWonCanvas;
    public Text remainingEnemies;
    public Text gameWonText;
    public Text timeText; 
    // Timer variables and enemies left variables
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
        // Check if all enemies are destroyed
        if (enemiesLeft == 0){
            UpdateWinText(timeUsed); // Display the time used
            gameWonCanvas.SetActive(true); // Display gameWon canvas
            // Display mainmenu scene after 5 seconds
            if (mainMenuTimer < 0){
                SceneManager.LoadScene("TitlePage");
            }
            else{
                mainMenuTimer -= Time.deltaTime;
            }
        }
        else{ // Displaying score on screen
            UpdateScoreText();
            timeUsed += Time.deltaTime;
        }
    }
    // Tracking destroyed enemies
    public void KilledEnemy(){
        enemiesLeft--;
        UpdateScoreText();
    }
    // Displaying score text on screen
    void UpdateScoreText(){
        remainingEnemies.text = string.Format("Enemies left:\n{0:0}", enemiesLeft);
    }
    // Displaying time used
    void UpdateWinText(float timeUsed){
        min = Mathf.FloorToInt(timeUsed / 60);
        sec = Mathf.FloorToInt(timeUsed % 60);
        miliSec = (timeUsed - Mathf.Floor(timeUsed))*1000;
        gameWonText.text = string.Format("Game Won!\n Your time was:\n{0:00}:{1:00}:{2:000}", min, sec, miliSec);
    }
    
}
