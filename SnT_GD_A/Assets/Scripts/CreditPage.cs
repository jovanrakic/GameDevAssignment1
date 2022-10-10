using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditPage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    // Exit to titlePage when escape is pressed
    if (Input.GetKeyDown("escape")){
        SceneManager.LoadScene("TitlePage");
        }  
    }
}
