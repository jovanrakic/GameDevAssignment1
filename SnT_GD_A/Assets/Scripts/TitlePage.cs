using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitlePage : MonoBehaviour {
    public void LoadLevel (string levelName) {
        SceneManager.LoadScene(levelName);
    }

    public void QuitGame() {
        Application.Quit ();
    }
}
