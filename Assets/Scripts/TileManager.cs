/*
 * Graphics and Interaction (COMP30019) 
 * Project 2: Endless Runner
 * Team: Karim Khairat, Duy (Daniel) Vu, and Brody Taylor
 * 
 * Manage tiles as player playground: Generate random tiles and populate powerups
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

    /*Tile Gameobjects!*/
	public GameObject leftTilePrefab;
	public GameObject topTilePrefab;
	public GameObject currTile;

	//Indices for Where tile spawn tTile = Top, lTile = Left
	private int tTile = 1;
	private int lTile = 2;

	private int MAXTILESRENDERED = 23;

	//Indices for the Powerups
    private int SLOWOBJ=1;
    private int FASTOBJ=2;
    private int PLUS5OBJ = 3;
    private int PLUS10OBJ = 4;
    private int PLUS50OBJ = 5;

	//Odds of receiving the powerup, 3/200 FAST, 1/200 SLOW, 3/200 +5, 9/1000 +10, 1/1000 +50
    private int FAST = 955;
    private int SLOW = 970;
    private int PLUS5 = 975;
    private int PLUS10 = 990;
    private int PLUS50 = 999;

    //Used to track current number of tiles rendered
    private GameObject[] TileTracker;
		
	/*on start, get the number of tiles and randomly create tiles*/
	void Start () {
		GetNumberofTiles ();
		rng();
	}
	/*on each update, if the number of tiles is less than 23, create more*/
	void Update () {
		if (GetNumberofTiles () <= MAXTILESRENDERED) {
			rng ();
		}
	}

	/* Gets the number of Tiles currently active */
	private int GetNumberofTiles(){
		TileTracker = GameObject.FindGameObjectsWithTag ("Tile");
		int count = TileTracker.Length;
		return count;
	}

	/* Randomly generate either a top or left tile */
	private void rng(){
		//randomly generate either 1 or 2. if 1  then generate a top tile, if 2 genearte a leftTile
		int i = Random.Range(tTile, lTile+1);
		if (i ==tTile) {
			CreateTopTile ();
		} else {
			CreateLeftTile ();
		}	
	}

	/* 
	 * Create top tile:
	 * First get child gets the Tile, 
	 * Second gets the anchor point (so in this case topanchorpt), quaternion.identity locks rotation :)
	 */
	public void CreateTopTile()
	{
		currTile = (GameObject)Instantiate (topTilePrefab,currTile.transform.GetChild(0).transform.GetChild(1).position,Quaternion.identity);
        PowerUP();
    }

	/* Create the left tile */
	public void CreateLeftTile()
	{
		currTile = (GameObject)Instantiate (leftTilePrefab,currTile.transform.GetChild(0).transform.GetChild(0).position,Quaternion.identity);
        PowerUP();
    }

    /* Add power up randomly if a valid number is randomed! */
    public void PowerUP()
    {

        int powerupRNG = Random.Range(0, 1000);
	//Create +1 Speed (aka Fast!) powerup(may be disadvantageous)!
        if (powerupRNG >= FAST && powerupRNG < SLOW)
        {
            currTile.transform.GetChild(FASTOBJ).gameObject.SetActive(true);
        }
	//Create -1 Speed powerup (aka Slow!)!
        else if (powerupRNG >= SLOW && powerupRNG <PLUS5)
        {
            currTile.transform.GetChild(SLOWOBJ).gameObject.SetActive(true);
        }
	//Create +5 powerup!
        else if (powerupRNG >= PLUS5 && powerupRNG < PLUS10)
        {
            currTile.transform.GetChild(PLUS5OBJ).gameObject.SetActive(true);
        }
	//Create +10 powerup!
        else if (powerupRNG >= PLUS10 && powerupRNG < PLUS50)
        {
            currTile.transform.GetChild(PLUS10OBJ).gameObject.SetActive(true);
        }
	//Create +50 powerup!
        else if (powerupRNG >= PLUS50)
        {
            currTile.transform.GetChild(PLUS50OBJ).gameObject.SetActive(true);
        }
	}
}
