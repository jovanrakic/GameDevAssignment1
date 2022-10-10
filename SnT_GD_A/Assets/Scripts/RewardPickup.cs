using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPickup : MonoBehaviour
{

    public Vector3 rotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotation);
        // rotation is a Vector3 (X,Y,Z) of the amount to rotate per update in degrees  
    }
    // Detect is object with "Player" tage colliders with other collider
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        { // Calling the reward fucntion from timer script
            GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>().ApplyRewardPickup();
            Destroy(gameObject); // Destroys game object if true
        }
      
    }
}
