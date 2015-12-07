using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {

	public GameObject nyt;
	public GameObject huff;
	public GameObject bbc;
	public GameObject cnn;
	MeshCollider coll;
	GameObject loc;

	// Use this for initialization
	void Start () {
		coll = GetComponent<MeshCollider> ();
		loc = GameObject.FindGameObjectWithTag ("surface");
	}

	void OnTriggerEnter (Collider col) {
		if (col.gameObject.tag == "Player") {
			Vector3 thingLoc = loc.transform.position;
			Instantiate(nyt, thingLoc, Quaternion.identity);
			Vector3 cnnLoc = new Vector3(thingLoc.x + 10,
			                         thingLoc.y + 5,
			                         thingLoc.z + 2);
			Instantiate(cnn, cnnLoc, Quaternion.identity);
			Vector3 bbcLoc = new Vector3(thingLoc.x + 18,
			                             thingLoc.y + 5,
			                             thingLoc.z + 1);
			Instantiate(bbc, bbcLoc, Quaternion.identity);
			Vector3 huffLoc = new Vector3(thingLoc.x+25,
			                              thingLoc.y + 3,
			                              thingLoc.z + -2);
			Instantiate(huff, huffLoc, Quaternion.identity);
		}
	}
}
