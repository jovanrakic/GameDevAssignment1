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
    // Timer to check enemies path
    public float pathCheckTime = 1.0f;
    private float elapsedPathCheckTime;
    // Explosion prefab
	public GameObject explosion;
    // Remaining enemy count
    public UpdateScore remainingEnemies;
    // Audio source for enemy attack
    AudioSource magic_03;


    /*
     * Initialize the Finite state machine for the NPC tank
     */
	void Start() {

        // Setting the default state to patrol
        curState = FSMState.Patrol;

        bDead = false;
        elapsedTime = 0.0f;

        // Get the target enemy(Player)
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        playerTransform = objPlayer.transform;

        remainingEnemies = GameObject.FindGameObjectWithTag("Score").GetComponent<UpdateScore>();

        if(!playerTransform)
            print("Player doesn't exist.. Please add one with Tag named 'Player'");

        elapsedPathCheckTime = pathCheckTime;

	}


    // Update each frame
    void Update() {
        // Check the current state at each frame
        switch (curState) {
            case FSMState.Patrol: UpdatePatrolState(); break;
            case FSMState.Chase: UpdateChaseState(); break;
            case FSMState.Attack: UpdateAttackState(); break;
            case FSMState.Dead: UpdateDeadState(); break;
        }
       
        
        // Update the time
        elapsedTime += Time.deltaTime;
        elapsedPathCheckTime += Time.deltaTime;
        
        // Go to dead state if no health left
        if (health <= 0){
            curState = FSMState.Dead;
        }
    }

	/*
     * Patrol state
     */
    protected void UpdatePatrolState() {

        // Setting the destination to waypoints
        nav = GetComponent<NavMeshAgent>(); 
        if (elapsedPathCheckTime >= pathCheckTime){
            nav.SetDestination(waypointList[currentWaypoint].transform.position);// Updating too much, half a second wait
            elapsedPathCheckTime = 0f;
            nav.isStopped = false;
        }

        // Transitions
        // Check the distance with player
        // When the distance is near, transition to chase state
        if (Vector3.Distance(transform.position, playerTransform.position) <= chaseRange) { // place above line 104
            curState = FSMState.Chase;
        }
        // Update waypoint if enemy reaches destination
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


        // Enemy rotation
        Quaternion enemyRotation = Quaternion.LookRotation(waypointList[currentWaypoint].transform.position - transform.position);
        enemyBody.transform.rotation = Quaternion.Slerp(enemyBody.transform.rotation, enemyRotation, Time.deltaTime * enemyBodyRotSpeed);
    }


    /*
     * Chase state
	 */
    protected void UpdateChaseState() {

        // Sets the destination to the player
        Vector3 playerPosition = new Vector3(playerTransform.transform.position.x,0, playerTransform.transform.position.z);
        if (Vector3.Distance(transform.position, playerPosition) > attackRangeStop)
        {
            nav = GetComponent<NavMeshAgent>();
            if (elapsedPathCheckTime >= pathCheckTime){
                nav.SetDestination(playerPosition);
                elapsedPathCheckTime = 0f;
                //nav.isStopped = false;
            }

        }
        else // Set destination to enemies current position
        {
            nav.SetDestination(gameObject.transform.position);
        }



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
        Vector3 playerPosition = new Vector3(playerTransform.transform.position.x,0, playerTransform.transform.position.z);
        if (Vector3.Distance(transform.position, playerPosition) > attackRangeStop)
        {
            nav = GetComponent<NavMeshAgent>();
            if (elapsedPathCheckTime >= pathCheckTime){
                nav.SetDestination(playerPosition);
                elapsedPathCheckTime = 0f;
            }

        }
        else // Sets the destination to its current position
        {
            nav.SetDestination(gameObject.transform.position);
        }

        // Transitions
        // Check the distance with the player 
        float dist = Vector3.Distance(transform.position, playerPosition);
		if (dist > attackRange) {
			curState = FSMState.Chase;
		}
        // Transition to patrol if the player is too far
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
            Debug.Log("Dead");
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
        Debug.Log("enemy hit! health is now at: " + health);
    }

    // Explosion for when enemy is dead
    protected void Explode() {
        float rndX = Random.Range(8.0f, 12.0f);
        float rndZ = Random.Range(8.0f, 12.0f);
        for (int i = 0; i < 3; i++) {
            GetComponent<Rigidbody>().AddExplosionForce(10.0f, transform.position - new Vector3(rndX, 2.0f, rndZ), 45.0f, 40.0f);
            GetComponent<Rigidbody>().velocity = transform.TransformDirection(new Vector3(rndX, 10.0f, rndZ));
        }

        // Spawn the explosion
		Invoke ("CreateFinalExplosion", 1.4f);
		Destroy(gameObject, 1.5f);
        remainingEnemies.KilledEnemy();
	}
	
	// Spawn the explosion
	protected void CreateFinalExplosion() {
		if (explosion) 
			Instantiate(explosion, transform.position, transform.rotation);

	}

    // Draw editor gizmos
	void OnDrawGizmos () {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, chaseRange);

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, attackRange);
	}

}
