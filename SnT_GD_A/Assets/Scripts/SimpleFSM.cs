using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class SimpleFSM : MonoBehaviour 
{
    public enum FSMState
    {
        None,
        Patrol,
        Chase,
        Attack,
        Dead,
    }

    private NavMeshAgent nav;

    // Current state that the NPC is reaching
    public FSMState curState;

	public Transform playerTransform;// Player Transform

    //Enemy path
    public GameObject[] waypointList;
    public int currentWaypoint = 0;
    // Enemy body
   // public GameObject turret;
	//public float turretRotSpeed = 4.0f;
    public GameObject enemyBody;
	public float enemyBodyRotSpeed = 4.0f;
	
    // Bullet
	public GameObject bullet;
	public GameObject bulletSpawnPoint;

	// Bullet shooting rate
	public float shootRate = 3.0f;
	protected float elapsedTime;

    // Whether the NPC is destroyed or not
    protected bool bDead;
    public int health = 100;

	// Ranges for chase and attack
	public float chaseRange = 35.0f;
	public float attackRange = 20.0f;
	public float attackRangeStop = 10.0f;

	public GameObject explosion;


    /*
     * Initialize the Finite state machine for the NPC tank
     */
	void Start() {


        curState = FSMState.Patrol;

        bDead = false;
        elapsedTime = 0.0f;

        // Get the target enemy(Player)
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        playerTransform = objPlayer.transform;

        if(!playerTransform)
            print("Player doesn't exist.. Please add one with Tag named 'Player'");

	}


    // Update each frame
    void Update() {
        switch (curState) {
            case FSMState.Patrol: UpdatePatrolState(); break;
            case FSMState.Chase: UpdateChaseState(); break;
            case FSMState.Attack: UpdateAttackState(); break;
            case FSMState.Dead: UpdateDeadState(); break;
        }
       
        
        // Update the time
        elapsedTime += Time.deltaTime;

        // Go to dead state if no health left
        if (health <= 0)
            curState = FSMState.Dead;
    }

	/*
     * Patrol state
     */
    protected void UpdatePatrolState() {

        // NavMeshAgent move code goes here
        nav = GetComponent<NavMeshAgent>();
        nav.SetDestination(waypointList[currentWaypoint].transform.position);
        nav.isStopped = false;
        if (Vector3.Distance(transform.position, waypointList[currentWaypoint].transform.position) <= 1)
        {
            if (currentWaypoint < 3)
            {
                currentWaypoint += 1;
            }
            else
            {
                currentWaypoint = 0;
            }
        
        }

            // Transitions
            // Check the distance with player
            // When the distance is near, transition to chase state
            if (Vector3.Distance(transform.position, playerTransform.position) <= chaseRange) {
            curState = FSMState.Chase;
        }

        Quaternion enemyRotation = Quaternion.LookRotation(waypointList[currentWaypoint].transform.position - transform.position);
        enemyBody.transform.rotation = Quaternion.Slerp(enemyBody.transform.rotation, enemyRotation, Time.deltaTime * enemyBodyRotSpeed);
    }


    /*
     * Chase state
	 */
    protected void UpdateChaseState() {

        // NavMeshAgent move code goes here
        nav = GetComponent<NavMeshAgent>();
        nav.SetDestination(playerTransform.transform.position);
        nav.isStopped = false;

        Debug.Log(Vector3.Distance(transform.position, playerTransform.transform.position));

        // Transitions
        // Check the distance with player tank
        // When the distance is near, transition to attack state
        float dist = Vector3.Distance(transform.position, playerTransform.position);
		if (dist <= attackRange) {
            curState = FSMState.Attack;
        }
        // Go back to patrol is it become too far
        else if (dist >= chaseRange) {
			curState = FSMState.Patrol;
		}
		
	}


    /*
	 * Attack state
	 */
    protected void UpdateAttackState() {

        if (Vector3.Distance(transform.position, playerTransform.transform.position) > attackRangeStop)
        {
            nav = GetComponent<NavMeshAgent>();
            nav.SetDestination(playerTransform.transform.position);
            nav.isStopped = false;
        }
        else
        {
            nav.isStopped = true;
        }

        // Transitions
        // Check the distance with the player 
        float dist = Vector3.Distance(transform.position, playerTransform.position);
		if (dist > attackRange) {
			curState = FSMState.Chase;
		}
        // Transition to patrol if the tank is too far
        else if (dist >= chaseRange) {
			curState = FSMState.Patrol;
		}

        // Always Turn the enemy towards the player
		
		Quaternion enemyRotation = Quaternion.LookRotation(playerTransform.position - transform.position);
        enemyBody.transform.rotation = Quaternion.Slerp(enemyBody.transform.rotation, enemyRotation, Time.deltaTime * enemyBodyRotSpeed); 
		

        // Shoot the bullets
        ShootBullet();
    }


    /*
     * Dead state
     */
    protected void UpdateDeadState() {
        // Show the dead animation with some physics effects
        if (!bDead) {
            bDead = true;
            Explode();
        }
    }


    /*
     * Shoot Bullet
     */
    private void ShootBullet() {
        if (elapsedTime >= shootRate) {
			if ((bulletSpawnPoint) & (bullet)) {
            	// Shoot the bullet
            	Instantiate(bullet, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
			}
            elapsedTime = 0.0f;
        }
    }

    // Apply Damage if hit by bullet
    public void ApplyDamage(int damage ) {
    	health -= damage;
    }


    protected void Explode() {
        float rndX = Random.Range(8.0f, 12.0f);
        float rndZ = Random.Range(8.0f, 12.0f);
        for (int i = 0; i < 3; i++) {
            GetComponent<Rigidbody>().AddExplosionForce(10.0f, transform.position - new Vector3(rndX, 2.0f, rndZ), 45.0f, 40.0f);
            GetComponent<Rigidbody>().velocity = transform.TransformDirection(new Vector3(rndX, 10.0f, rndZ));
        }


		Invoke ("CreateFinalExplosion", 1.4f);
		Destroy(gameObject, 1.5f);
	}
	
	
	protected void CreateFinalExplosion() {
		if (explosion) 
			Instantiate(explosion, transform.position, transform.rotation);
	}


	void OnDrawGizmos () {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, chaseRange);

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, attackRange);
	}

}
