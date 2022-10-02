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
    void Awake()
    {
	bulletRigidbody= GetComponent<Rigidbody>();

    }

    void Start()
    {
	bulletRigidbody.velocity = transform.forward *speed;
    GameObject enemyObject = GameObject.FindGameObjectWithTag("Enemy");
    if (enemyObject != null)
        {
        enemy = enemyObject.GetComponent<SimpleFSM>();
        if (enemy == null)
            {
            enemy_freeze = enemyObject.GetComponent<SimpleFSMFreeze>();
            isFreeze = true;
            }
        }
    else 
        {
        Debug.Log("EnemyObject not found");
        }
    }
    
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy") {
            if (isFreeze){
                enemy_freeze.ApplyDamage(10);
            }
            else{
                enemy.ApplyDamage(10);
            }
        }
    Destroy(gameObject);
    }
}
