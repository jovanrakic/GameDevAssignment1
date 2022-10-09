using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitlePage : MonoBehaviour {
public AudioSource musicControl;

    public void Awake() {
        Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
        if (!GameObject.FindGameObjectWithTag("MusicControl").GetComponent<AudioSource>().isPlaying)
            GameObject.FindGameObjectWithTag("MusicControl").GetComponent<AudioSource>().Play();
    }
    public void LoadLevel (string levelName) {
        GameObject.FindGameObjectWithTag("MusicControl").GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene(levelName);
        
    }

    public void ShowCredits(){
        SceneManager.LoadScene("Credits");
    }
    public void ShowHelp(){
        SceneManager.LoadScene("Help");
    }

    public void QuitGame() {
        Application.Quit ();
    }
}
