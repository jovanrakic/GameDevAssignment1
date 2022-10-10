using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    // Current time variables
    public float curTime = 75; 
    public float min; 
    public float sec; 
    public float miliSec; 
    public Text remainingTime; 
    public Text totalTime;
    private float damageTimer;
    private float gameOverTimer;
    public float timeUsed;
    // GUI canvases
    public GameObject gameOverCanvas;
    public GameObject gameWonCanvas;
    // Flags to keep track of cool down state and gamewon and gameover state
    private bool coolDown = false;
    private bool gameWon = false;

    
    // Update is called once per frame
    void Update(){
        if (!gameWon){ // If game is ongoing
        if (coolDown == true){ // Check if cooldown is true
            if (damageTimer < 2){ // Check if damagetimer is less than 2 seconds
               damageTimer += Time.deltaTime; // Increment damagetimer
                    } else { // Turn off cooldown state and reset damagetimer
                        coolDown = false; 
                        damageTimer = 0;
                    }
                }
            if (curTime > 0){// if current time is greater then zero
                curTime -= Time.deltaTime; // decrement time
            }
            else{ // Time ran out, show gameOver canvas
                curTime = 0;
                gameOverCanvas.SetActive(true);
        

                if (gameOverTimer < 5) { // If gameover time is less than 5 seconds
                    gameOverTimer += Time.deltaTime; // Increment gameover timer
                } 
                else { // When gameOver timer reaches 0, Game restarts
                    gameOverTimer = 0;
                    RestartGame();
                }
            }
            UpdateTime(curTime); // Display current time on screen
            timeUsed += Time.deltaTime; // Increment time used
        }
        else{
            UpdateWinText(timeUsed); //Display timeUsed on screen
            gameWonCanvas.SetActive(true); // Displays gameWon canvas
        }
    }
    // Displays the remaining time on screen
    void UpdateTime(float timeLeft){
        min = Mathf.FloorToInt(timeLeft / 60);
        sec = Mathf.FloorToInt(timeLeft % 60);
        miliSec = (timeLeft - Mathf.Floor(timeLeft))*1000;
        remainingTime.text = string.Format("Time Left:\n{0:00}:{1:00}:{2:000}", min, sec, miliSec);
    }
    // Displays the timeUsed on screen
    void UpdateWinText(float timeUsed){
        min = Mathf.FloorToInt(timeUsed / 60);
        sec = Mathf.FloorToInt(timeUsed % 60);
        miliSec = (timeUsed - Mathf.Floor(timeUsed))*1000;
        totalTime.text = string.Format("Game Won!\nTime used: {0:00}:{1:00}:{2:000}", min, sec, miliSec);
    }
    // Restarts current level
    public void RestartGame() { 
        //Debug.Log("Restart");
        SceneManager.LoadScene("Main");
    }
    // Reward function to add 90 seconds to timer
    public void ApplyRewardPickup(){
        curTime += 90;
    }
    // Damage function takes 10 second of current timer
    public void TakeDamage() {
    if (coolDown == false) {
           curTime -= 10;
           coolDown = true;
        } 
 
    }
}