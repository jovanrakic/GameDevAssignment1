using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class ExplosionFreeze : MonoBehaviour
{
    public ThirdPersonController player;
    public float freezeTime = 2.0f;
    public bool isHit = false;
    AudioSource magic_03;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerBody").GetComponent<ThirdPersonController>();
        magic_03 = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isHit){
            if (freezeTime > 0)
            {
                freezeTime -= Time.deltaTime;
            }
            else 
            {
                player.MoveSpeed = 2.0f;
                player.SprintSpeed = 5.335f;
                freezeTime = 2.0f;
                isHit = false;
            }
        }
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerBody") {
            player.MoveSpeed = 0.0f;
            player.SprintSpeed = 0.0f;
            isHit = true;
            magic_03.Play();
        }
    }
}

