using UnityEngine;
using System.Collections;

public class ThirdPersonSpawn : MonoBehaviour {

	public Transform[] spawnpoints;
	Transform personTransform;

	// Use this for initialization
	void Start () {
		personTransform = GetComponent<Transform> ();
		setInitialPosition ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void setInitialPosition() {
		// put player at a spawnpoint
		int randomSpot = Random.Range (0, spawnpoints.Length);
		// set our cop at the position;
		personTransform.position = spawnpoints [randomSpot].position;
	}
}

