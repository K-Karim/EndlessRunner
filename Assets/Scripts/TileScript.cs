using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileScript : MonoBehaviour {

	Tracker tracker;
	// Use this for initialization
	void Start () {
		tracker = new Tracker();
	}
	
	// Update is called once per frame
	void Update () {
	}
	void OnBecameInvisible () {
		Destroy (this.gameObject);
		tracker.settracker(tracker.gettracker()-1);
		Debug.Log (tracker.gettracker());
	}


}
