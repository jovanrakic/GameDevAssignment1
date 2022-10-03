using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody bulletRigidbody;
    public float speed = 10f;
    private SimpleFSM enemy;
    private SimpleFSMFreeze enemy_freeze;
    private bool isFreeze;
    public GameObject enemyImpact;
    public GameObject generalImpact;
    void Awake()
    {
	bulletRigidbody= GetComponent<Rigidbody>();

    }

    void Start()
    {
	    bulletRigidbody.velocity = transform.forward *speed;
    }
    
    
    void OnTriggerEnter(Collider other)
    {
        GameObject enemyObject = other.gameObject;
        if (enemyObject != null)
        {
            enemy = enemyObject.GetComponent<SimpleFSM>();
            if (enemy == null)
            {
                enemy_freeze = enemyObject.GetComponent<SimpleFSMFreeze>();
                isFreeze = true;
            }
        }
        if (other.gameObject.tag == "Enemy") {
            Instantiate(enemyImpact, transform.position, transform.rotation);
            if (isFreeze){
                enemy_freeze.ApplyDamage(10);
            }
            else{
                enemy.ApplyDamage(10);
            }
        } 
        else {
            Instantiate(generalImpact, transform.position, transform.rotation);
        }
    Destroy(gameObject);
    }
}
