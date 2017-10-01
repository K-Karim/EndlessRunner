using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

	public GameObject leftTilePrefab;
	public GameObject topTilePrefab;
	public GameObject currTile;
	private int tTile = 1;
	private int lTile = 2;
	Tracker T;
	// Use this for initialization
	void Start () {
		T = new Tracker();
		rng();

	}
	
	// Update is called once per frame
	void Update () {

		if (T.gettracker() <= 30) {
			Debug.Log (T.gettracker());
			rng ();
			increment ();
		}

	}
	void increment(){
		T.settracker(T.gettracker() + 1);
	}
	void decrement(){
		T.settracker(T.gettracker() -1);
	}

	//Randomly generate either a top or left tile
	private void rng(){
		//randomly generate either 1 or 2. if 1  then generate a top tile, if 2 genearte a leftTile
		int i = Random.Range(tTile, lTile+1);
		if (i ==tTile) {
			CreateTopTile ();
		} else {
			CreateLeftTile ();
		}	
	}


	public void CreateTopTile()
	{
		//first get child gets the Tile, second gets the anchor point (so in this case topanchorpt), quaternion.identity locks rotation :)
		currTile = (GameObject)Instantiate (topTilePrefab,currTile.transform.GetChild(0).transform.GetChild(1).position,Quaternion.identity);
	}
	public void CreateLeftTile()
	{
		currTile = (GameObject)Instantiate (leftTilePrefab,currTile.transform.GetChild(0).transform.GetChild(0).position,Quaternion.identity);
	}

}
