

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	//Starting Speed will be set in unity!
	public float speed;
	private Vector3 direction;

	// Use this for initialization
	void Start () {
		//doesn't move until user presses/ picks location
		direction = Vector3.zero;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.W)|| Input.GetKeyDown (KeyCode.UpArrow)) {
			direction = Vector3.forward;
		}
		else if( Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown (KeyCode.LeftArrow)){
			direction = Vector3.left;

		}
		else if( Input.GetKeyDown(KeyCode.D)|| Input.GetKeyDown (KeyCode.RightArrow)){
			direction = Vector3.right;

		}
		else if( Input.GetKeyDown(KeyCode.Space)){
			direction = Vector3.left;

		}
		//Speed and distance moved :)
		float moved = speed * Time.deltaTime;
		transform.Translate(direction * moved);
	}
}
