using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour {
	public int tracker;
	// Use this for initialization
	void Start(){
		tracker = 0;
	}
	void Update(){

	}

	public int gettracker(){
		return tracker;
	}
	public void increment(){
		tracker++;
	}
	public void decrement(){
		tracker--;
	}
}
