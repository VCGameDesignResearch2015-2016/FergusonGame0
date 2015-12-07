using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

	GameObject HUDCanvas;
	Animator anim;
	GameObject cop;
	AIPatrolMovement copPatrol;
	bool copInRange;
	GameObject[] cops;
	GameObject colHit;
//	AIPatrolMovement copMovement;

	void Start () {
		HUDCanvas = GameObject.FindGameObjectWithTag ("HUDCanvas");
		anim = HUDCanvas.GetComponent<Animator> ();
		cops = GameObject.FindGameObjectsWithTag ("Cop");
	}

	void OnCollisionEnter (Collision col) {
		colHit = col.gameObject;
		for (int i= 0; i < cops.Length; i++) {
			if (cops [i] == colHit) {
				copInRange = true;
//				copMovement = colHit.GetComponent<AIPatrolMovement>();
			}
		}
	}

	void Update () {
		if (copInRange) {
			anim.SetTrigger ("EnemyCollision");
//			copMovement.doWeSeePlayer = false;
			copInRange = false;
			anim.SetTrigger("BackToGame");
		}
	}

}
