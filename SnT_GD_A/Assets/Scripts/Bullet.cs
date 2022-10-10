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
        //Set speed for transform movement and move transform forward
	    bulletRigidbody.velocity = transform.forward *speed;
    }
    
    
    void OnTriggerEnter(Collider other)
    {
        GameObject enemyObject = other.gameObject;
        // Check if object collide with an actual object
        if (enemyObject != null)
        {
            //retrieve enemy fsm script
            enemy = enemyObject.GetComponent<SimpleFSM>();
            //check if normal or freeze enemy
            if (enemy == null)
            {
                enemy_freeze = enemyObject.GetComponent<SimpleFSMFreeze>();
                isFreeze = true;
            }
        }

        //If object is enemy
        if (other.gameObject.tag == "Enemy") {
            Instantiate(enemyImpact, transform.position, transform.rotation);
            if (isFreeze){
                if (enemy_freeze != null){
                    enemy_freeze.ApplyDamage(10);
                }
            }
            else{
                if (enemy !=null){
                    enemy.ApplyDamage(10);
                }
            }
        } 
        else {
            Instantiate(generalImpact, transform.position, transform.rotation);
        }
    //destroy bullet
    Destroy(gameObject);
    }
}
