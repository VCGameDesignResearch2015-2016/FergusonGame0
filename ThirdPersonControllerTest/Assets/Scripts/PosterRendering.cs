using UnityEngine;
using System.Collections;

public class PosterRendering : MonoBehaviour {

	public float timeBetweenBullets = 1.0f;
	public float range = 100.0f;
	public GameObject poster;

	float timer;
	Ray shootRay;
	int shootableMask;
	float smallDiff = 0.01f;
	Vector3 posterPos;
//	Transform playerTransform; 

	void Awake ()
	{
		shootableMask = LayerMask.GetMask ("Shootable");
//		playerTransform = GetComponent<Transform> ();
	}


	void FixedUpdate ()
	{	
		timer += Time.deltaTime;

		if (Input.GetButton ("Fire1") && timer > timeBetweenBullets) {
			shootRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit shootHit;
			if (Physics.Raycast (shootRay, out shootHit, range, shootableMask)) {
				if (shootHit.collider.tag == "canTag") {
					Transform hitTransform = shootHit.transform;
					Quaternion rotation = hitTransform.rotation;
//					posterPos.x = shootHit.point.x + smallDiff;
//					posterPos.y = shootHit.point.y + smallDiff;
//					posterPos.z = shootHit.point.z + smallDiff;
					posterPos = shootHit.point;
					Instantiate (poster, posterPos, rotation);
					timer = 0;
				}
			}
		}
	}
}
	