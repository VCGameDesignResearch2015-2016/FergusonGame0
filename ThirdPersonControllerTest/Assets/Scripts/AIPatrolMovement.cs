using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class AIPatrolMovement : MonoBehaviour {

	public int chaseSpeed = 10;
	public int patrolSpeed = 5;
	public Transform[] checkpoints;
	public float range = 100f;
	public bool doWeSeePlayer;

	// what the enemy is moving toward
	Transform playerTransform;
	Transform enemyTransform;
	int checkpointIndex;
	bool areWeFollowingPlayer;
	int shootableMask;
	Ray shootRay;
	RaycastHit shootHit;
	AICharacterControl characterControl;

	// Use this for initialization
	void Start () {
	
		characterControl = this.GetComponent<AICharacterControl> ();
		// where the player is
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform; 
		enemyTransform = GetComponent<Transform> ();
		areWeFollowingPlayer = false;
		shootableMask = LayerMask.GetMask ("Shootable");
		setInitialCheckpointAndPosition ();
		// to start our cop on his patrol
		continuePatrol();
	}
	
	// Update is called once per frame
	void Update () {
		lookForPlayer ();
		// if we see the player, then follow the player
		if (doWeSeePlayer == true) {
			followPlayer ();
		}
		// if we were just following the player, but don't see the player anymore
		// go back on patrol
		else {
			if (areWeFollowingPlayer) {
				goBackOnPatrol ();
			}
		// if we didn't see the player and are not following the player,
		// then we are on patrol, which is implemented with the onTrigger & continuePatrol stuff
		// so we don't need to do anything.
			else {
				return;
			}
		}
	}

	void OnCollisionEnter (Collision col) {
		if (col.gameObject.tag == "Cop") {
			goBackOnPatrol();
		}
	}

	void setInitialCheckpointAndPosition() {
		checkpointIndex = Random.Range (0, checkpoints.Length);
		// set our cop at the position;
		enemyTransform.position = checkpoints [checkpointIndex].position;

		// Setting the target
		//characterControl.target = checkpoints [checkpointIndex];
	}

	void followPlayer() {
		areWeFollowingPlayer = true;
		characterControl.agent.speed = chaseSpeed;
		characterControl.target = playerTransform;
	}

	public void goBackOnPatrol() {
		areWeFollowingPlayer = false;
		// so that it errors if the length is zero --> something must be there for the cop
		// to go to
		int goToCheckpoint = -100;
		float minimumDistance = 100000000;
		// see which checkpoint we are closest to and set our destination to that checkpoint
		// using for instead of foreach so we can set the checkpointIndex too
		for (int i = 0; i < checkpoints.Length; i++) {
			// get the distance from the enemy player
			float distance = Vector3.Distance(checkpoints[i].position, enemyTransform.position);
			// if less than minimumDistance, then make the checkpoint the one we go to. 
			//			Debug.Log (distance);
			if (distance < minimumDistance) {
				minimumDistance = distance;
				goToCheckpoint = i;
			}
		}
		// if two checkpoints are equally distance from each other, then only the first
		// will be taken, but that's okay because this is an approximation. 
		checkpointIndex = goToCheckpoint;
		continuePatrol ();
	}
	
	// if it's time to turn... set the next destination as the next checkpoint
	// but first we are making sure we already hit the right checkpoint that was set before
	void OnTriggerEnter(Collider collider) {
		// First check we hit the right collider..
		// if hit the right collider, go back on patrol
		if (collider.transform == checkpoints [checkpointIndex - 1]) {
			continuePatrol ();
		} 
		// otherwise we do nothing because we haven't gotten to our checkpoint yet
	}
	
	void continuePatrol() {
		// To continue our patrol, set our destination to the next checkpoint
		
		// When we enter a collider, set destination to the next collider's position
		if (checkpointIndex >= checkpoints.Length) {
			checkpointIndex = 0;
		}
		// set next destination
		characterControl.agent.speed = patrolSpeed;
		characterControl.target = checkpoints [checkpointIndex];
		checkpointIndex++;
	}

	bool lookForPlayer() {
		// creating a vector from the enemy to the player
		Vector3 direction = (playerTransform.position - enemyTransform.position).normalized;
		
		/// setting it's stuff
		shootRay.origin = enemyTransform.position;
		shootRay.direction = direction;
		RaycastHit shootHit;
		
		//if there is any vector unobstructed from the enemy to the player
		if (Physics.Raycast (shootRay, out shootHit, 100.0f, shootableMask)) {
			// i.e. the first thing the ray hits is the player, then true --> if not false
			return shootHit.collider.tag == "Player";
			doWeSeePlayer = true;
		} else {
			// if the ray doesn't hit anything, then the cop must not see the player
			doWeSeePlayer = false;
			return false;
		}
	}

}
