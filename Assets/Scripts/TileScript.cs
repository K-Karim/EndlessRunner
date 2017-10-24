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

	/* Destroy when the square becomes invisible. */
	void OnBecameInvisible () {
		if (!GetComponent<Rigidbody> ().isKinematic) {
			Destroy (this.gameObject);
		}
	}

	/* After player exits square, make the squares falldown*/
	void OnTriggerExit(Collider collider){
		if (collider.tag == "Player") {
  		    //By setting isKinematic to false, isGravity will pull the square downn!
        	    GetComponent<Rigidbody>().isKinematic = false;
        }

    }



}
