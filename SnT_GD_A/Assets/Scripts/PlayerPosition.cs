using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Grabbing players X and Z position and keeping y as 0
        transform.position = new Vector3(player.transform.position.x,0, player.transform.position.z);
    }
}
