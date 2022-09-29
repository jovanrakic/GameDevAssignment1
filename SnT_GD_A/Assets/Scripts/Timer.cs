using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float curTime = 75;
    public float min;
    public float sec;
    public float miliSec;
    public Text remainingTime;
    private float damageTimer;

    private bool coolDown = false;

    // Update is called once per frame
    void Update(){
        if (curTime > 0){
            curTime -= Time.deltaTime;
        }
        else{
            curTime = 0;
        }
        UpdateTime(curTime);
        if (coolDown == true){
            if (damageTimer < 2){
                damageTimer += Time.deltaTime;
            } else {
                coolDown = false;
                damageTimer = 0;
            }
        }
    }

    void UpdateTime(float timeLeft){
        min = Mathf.FloorToInt(timeLeft / 60);
        sec = Mathf.FloorToInt(timeLeft % 60);
        miliSec = (timeLeft - Mathf.Floor(timeLeft))*1000;
        remainingTime.text = string.Format("Time Left:\n{0:00}:{1:00}:{2:000}", min, sec, miliSec);
    }

    public void TakeDamage() {
    if (coolDown == false) {
           curTime -= 10;
           coolDown = true;
        } 
 
    }
}