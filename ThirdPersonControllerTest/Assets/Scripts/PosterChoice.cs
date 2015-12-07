using UnityEngine;
using System.Collections;

public class PosterChoice : MonoBehaviour {

	GameObject player;
	Animator anim;
	PosterRendering posterRenderingScript;
	public GameObject poster1;
	public GameObject poster2;
	public GameObject poster3;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
		posterRenderingScript = player.GetComponent <PosterRendering> ();
		anim = GetComponent<Animator> ();
	}

	void Update () {
		if (Input.GetKey (KeyCode.K)) {
			anim.SetTrigger ("PosterChoice");
		}
	}

	void FixedUpdate (){
		PickPoster ();
	}

	void PickPoster() {
		if (Input.GetKey (KeyCode.Alpha1)) {
			posterRenderingScript.poster = poster1;
		} else {
			if (Input.GetKey (KeyCode.Alpha2)) {
				posterRenderingScript.poster = poster2;
			} else {
				if (Input.GetKey (KeyCode.Alpha3)) {
					posterRenderingScript.poster = poster3;
				} else {
					return;
				}
			}
		}
		anim.SetTrigger ("ChoiceOff");
	}
}