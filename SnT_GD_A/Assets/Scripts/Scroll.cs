using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scroll : MonoBehaviour
{
    public Transform creditTransform; // Credit text refernece
    private float changeSceneTimer = 75; // Set scene timer to 75 seconds
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Scrolling the credit text up
        creditTransform.position += new Vector3(0,30*Time.deltaTime,0);
        // When timer reaches 0, load mainmenu scene
        if (changeSceneTimer < 0){
            SceneManager.LoadScene("TitlePage");
        }
        else{
            changeSceneTimer -= Time.deltaTime;
        }
    }
}
