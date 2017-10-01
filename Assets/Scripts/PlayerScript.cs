

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

	//Starting Speed will be set in unity!
	public float speed;
	private Vector3 direction;
	private int Score=0;
	public Text ScoreTxt;
	// Use this for initialization
	void Start () {
		//doesn't move until user presses/ picks location
		Score = 0;
		direction = Vector3.zero;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow)) {
			direction = Vector3.forward;
		} else if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.LeftArrow)) {
			direction = Vector3.left;

		} else if (Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown (KeyCode.RightArrow)) {
			direction = Vector3.right;

		} else if (Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown (KeyCode.DownArrow)) {
			direction = Vector3.down;
		}

		//space = JUMP, however causes complications atm.
		else if( Input.GetKeyDown(KeyCode.Space)){
			//direction = Vector3.up;

		}
		//Speed and distance moved :)
		float moved = speed * Time.deltaTime;
		transform.Translate(direction * moved);

		}
	//Once you exit the tile, you get a point! 
	void OnTriggerExit(Collider tile)
	{
		if(tile.tag == "Tile"){
			Score++;
			ScoreTxt.text = Score.ToString ();
		}
	}

}
