using UnityEngine;
using System.Collections;

public class ClickingMechanic : MonoBehaviour {

	public float timeBetweenBullets = 1.0f;
	public float range = 100.0f;
	public Camera playerCam;
	public Camera mbCamera;

	public GameObject posterSurface;
	public GameObject[] articles;
	
	float timer=0;
	Ray shootRay;
	int shootableMask;
	GameObject poster;
	
	void Awake ()
	{
		shootableMask = LayerMask.GetMask ("Shootable");
		playerCam.enabled = true;
		mbCamera.enabled = false;
	}
	
	
	void Update ()
	{	
		timer += Time.deltaTime;
		int rand = Random.Range (0, articles.Length);
		poster = articles [rand];
		if (Input.GetButton ("Fire1") && timer > timeBetweenBullets) {
			shootRay = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit shootHit;
			if (Physics.Raycast (shootRay, out shootHit, range, shootableMask)) {
				if (shootHit.collider.tag == "michaelBrown") {
					playerCam.enabled = false;
					mbCamera.enabled = true;
					showArticle(poster);
				}
			}
		}
		if (Input.GetKeyDown ("space")) {
			playerCam.enabled = true;
			mbCamera.enabled = false;
			deleteArticle(poster);
		}
	}

	void showArticle(GameObject post) {
		Instantiate (post, posterSurface.transform.position, Quaternion.identity);
	}

	void deleteArticle(GameObject post) {
		Destroy (post);
	}

}
