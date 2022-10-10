using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class ExplosionFreeze : MonoBehaviour
{
    public ThirdPersonController player; // Accessing the player controller
    public float freezeTime = 2.0f; // Setting freezeTime to 2 seconds
    public bool isHit = false; // Setting ishit to false
    AudioSource magic_03; // Audio source for the freeze damage

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<ThirdPersonController>(); // Getting the player reference
        magic_03 = GetComponent<AudioSource>();// Getting the audio source reference
    }

    void Update()
    {
        if (isHit){ // If player is hit
            if (freezeTime > 0) // Decrement the freezeTime
            {
                freezeTime -= Time.deltaTime;
            }
            else // Reset movement speeds of the player
            {
                player.MoveSpeed = 2.0f;
                player.SprintSpeed = 5.335f;
                freezeTime = 2.0f;
                isHit = false;
            }
        }
    }
    // When player walks in trigger zone
    void OnTriggerEnter(Collider other)
    {
    magic_03.Play();
        // Check if the collider belongs to the player
        if (other.gameObject.tag == "PlayerBody") {
            // Set the movement speeds to zero
            player.MoveSpeed = 0.0f;
            player.SprintSpeed = 0.0f;
            isHit = true;
            
        }
    }
}

