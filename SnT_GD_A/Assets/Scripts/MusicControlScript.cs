using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControlScript : MonoBehaviour
{
    public static MusicControlScript instance;
    private void Awake()
    { // Destroy music gameObject if on main scene
        DontDestroyOnLoad(this.gameObject);

        if (instance == null){
            instance = this;
        } else{
           Destroy(gameObject);
        }
    }
}
