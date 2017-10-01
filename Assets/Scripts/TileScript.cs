using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileScript : MonoBehaviour {

	Tracker T;
	// Use this for initialization
	void Start () {
		T = new Tracker();
	}
	
	// Update is called once per frame
	void Update () {
	}
	void OnBecameInvisible () {
		Destroy (this.gameObject);
		decrement();
		Debug.Log (T.tracker);
	}
	void decrement(){
		T.tracker-=1;
	}

}
