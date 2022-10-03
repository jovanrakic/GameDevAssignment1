using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scroll : MonoBehaviour
{
    public Transform creditTransform;
    private float changeSceneTimer = 75;
    // Start is called before the first frame update
    void Start()
    {
//        Vector3 transformPos = creditTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        creditTransform.position += new Vector3(0,30*Time.deltaTime,0);
        if (changeSceneTimer < 0){
            SceneManager.LoadScene("TitlePage");
        }
        else{
            changeSceneTimer -= Time.deltaTime;
        }
    }
}
