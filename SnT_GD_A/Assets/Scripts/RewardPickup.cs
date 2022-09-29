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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>().ApplyRewardPickup();
            Destroy(gameObject);
        }
      
    }
}
