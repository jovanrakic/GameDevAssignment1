using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitlePage : MonoBehaviour {
    public void Awake() {
        Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
    }
    public void LoadLevel (string levelName) {
        SceneManager.LoadScene(levelName);
    }
    public void ShowHelp(){
        SceneManager.LoadScene("Help");
    }

    public void QuitGame() {
        Application.Quit ();
    }
}
