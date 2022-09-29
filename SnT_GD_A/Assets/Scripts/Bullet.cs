using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody bulletRigidbody;
    public float speed = 10f;
    private SimpleFSM enemy;
    void Awake()
    {
	bulletRigidbody= GetComponent<Rigidbody>();

    }

    void Start()
    {
	bulletRigidbody.velocity = transform.forward *speed;
    enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<SimpleFSM>();
    }
    
    
    void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.tag == "Enemy") {
        enemy.ApplyDamage(10);
        }
    Destroy(gameObject);
    }
}
