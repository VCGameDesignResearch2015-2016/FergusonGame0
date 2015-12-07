using UnityEngine;
using System.Collections;

public class vidscripy : MonoBehaviour {

	void Update () {
		if (Input.GetButtonDown ("Jump")) {
			
			Renderer r = GetComponent<Renderer>();
			MovieTexture movie = (MovieTexture)r.material.mainTexture;
			
			if (movie.isPlaying) {
				movie.Pause();
			}
			else {
				movie.Play();
			}
		}
	}
}
