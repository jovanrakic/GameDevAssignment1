using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float curTime = 75;
    public float min;
    public float sec;
    public float miliSec;
    public Text remainingTime;
    public Text totalTime;
    private float damageTimer;
    public GameObject gameOverCanvas;
    public GameObject gameWonCanvas;
    private bool coolDown = false;
    private bool gameWon = false;
    private float gameOverTimer;
    public float timeUsed;
    // Update is called once per frame
    void Update(){
        if (!gameWon){
        if (coolDown == true){
            if (damageTimer < 2){
               damageTimer += Time.deltaTime;
                    } else {
                        coolDown = false;
                        damageTimer = 0;
                    }
                }
            if (curTime > 0){
                curTime -= Time.deltaTime;
            }
            else{
                curTime = 0;
                gameOverCanvas.SetActive(true);
        

                if (gameOverTimer < 5) {
                    gameOverTimer += Time.deltaTime;
                } 
                else {
                    gameOverTimer = 0;
                    RestartGame();
                }
            }
            UpdateTime(curTime);
            timeUsed += Time.deltaTime;
        }
        else{
            UpdateWinText(timeUsed);
            gameWonCanvas.SetActive(true);
        }
    }

    void UpdateTime(float timeLeft){
        min = Mathf.FloorToInt(timeLeft / 60);
        sec = Mathf.FloorToInt(timeLeft % 60);
        miliSec = (timeLeft - Mathf.Floor(timeLeft))*1000;
        remainingTime.text = string.Format("Time Left:\n{0:00}:{1:00}:{2:000}", min, sec, miliSec);
    }

    void UpdateWinText(float timeUsed){
        min = Mathf.FloorToInt(timeUsed / 60);
        sec = Mathf.FloorToInt(timeUsed % 60);
        miliSec = (timeUsed - Mathf.Floor(timeUsed))*1000;
        totalTime.text = string.Format("Game Won!\nTime used: {0:00}:{1:00}:{2:000}", min, sec, miliSec);
    }

    public void RestartGame() {
        //Debug.Log("Restart");
        SceneManager.LoadScene("Main");
    }
    public void ApplyRewardPickup(){
        curTime += 60;
    }

    public void TakeDamage() {
    if (coolDown == false) {
           curTime -= 10;
           coolDown = true;
        } 
 
    }
}