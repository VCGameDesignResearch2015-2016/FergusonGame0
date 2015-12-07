using UnityEngine;
using System.Collections;

public class StopMovement : MonoBehaviour {

	PlayerMovement playermove;
	public GameObject player;

	// Use this for initialization
	void Start () {
		playermove = player.GetComponent<PlayerMovement>();
	
	}
	
	void onTriggerEnter(Collider coll) {
		if (coll.gameObject.tag == "Player") {
			playermove.enabled = false;
		}
	
	}
}
