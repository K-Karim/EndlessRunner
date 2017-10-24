/*
 * Graphics and Interaction (COMP30019) 
 * Project 2: Endless Runner
 * Team: Karim Khairat, Duy (Daniel) Vu, and Brody Taylor
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileScript : MonoBehaviour {
	private float delay = 0f;

	private static TileScript tilescript;

	// NOT CURRENTLY USED, PRE-EMPTIVELY SET IN CASE NEEDED LATER, ALLOWS US TO USE THE CLASS IN ANOTHER CLASS
	public static TileScript Tilescript {
		get {
			if (tilescript == null) {
				tilescript = GameObject.FindObjectOfType<TileScript> ();

			}
			return tilescript;
		}
	}

	void Start () {
	}
	
	void Update () {
	}

	/* Destroy when the square becomes invisible. */
	void OnBecameInvisible () {
		if (!GetComponent<Rigidbody> ().isKinematic) {
			Destroy (this.gameObject);
		}
	}

	/* After player exits square,trigger FallDown, which makes squares fall after a delay */
	void OnTriggerExit(Collider collider){
		if (collider.tag == "Player") {
            GetComponent<Rigidbody>().isKinematic = false;
        }

    }

	/* Make square fall down after a delay */
	IEnumerator FallDown()
	{
		// Wait the specified delay period before dropping squares!
		yield return new WaitForSeconds (delay);
		//By setting isKinematic to false, isGravity will pull the square downn!
		GetComponent<Rigidbody> ().isKinematic = false;
	}

}
