using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
   public Timer timer;
   AudioSource magic_03;
    void Start()
    {
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
        magic_03 = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
    magic_03.Play();
        if (other.gameObject.tag == "Player") {
            timer.TakeDamage();
        }
    }
}
