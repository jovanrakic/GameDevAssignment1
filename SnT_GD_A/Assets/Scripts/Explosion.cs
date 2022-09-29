using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
   public Timer timer;
    void Start()
    {
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
        timer.TakeDamage();
        }
    }
}
