using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
   public Timer timer;
   AudioSource magic_03; // Audio source for the freeze damage
    void Start()
    {
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>(); // Getting the player reference
        magic_03 = GetComponent<AudioSource>(); // Getting the audio source reference
    }

    // When player walks in trigger zone
    void OnTriggerEnter(Collider other)
    {
    magic_03.Play();
        // Check if the collider belongs to the player
        if (other.gameObject.tag == "Player") {
            timer.TakeDamage();
        }
    }
}
