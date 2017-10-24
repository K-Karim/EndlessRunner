/*
 * Graphics and Interaction (COMP30019) 
 * Project 2: Endless Runner
 * Team: Karim Khairat, Duy (Daniel) Vu, and Brody Taylor
 * 
 * 
 */

using UnityEngine;
using System.Collections;

public class InfiniteTerrain : MonoBehaviour
{
	public GameObject Player;
	public GameObject[] seas;
	private GameObject currSea;
	private GameObject leftSea;
	private GameObject topSea;
	private GameObject diagSea;
	//Inital distance between player and adjacent sea gameobjects
	private double distance;
	//Spacing between adjacent sea gameobjects
	private float spacing;
	//Whether the sea gameObjects have updated to their new positions
	private bool hasUpdated = true;
	//Y position of the sea gameObjects
	private float Y_POS_SEA;

	/*Initalization*/
	void Start ()
	{
		currSea = seas [0];
		leftSea = seas [1];
		topSea = seas [2];
		diagSea = seas [3];

		Y_POS_SEA = currSea.transform.position.y;
		distance = Mathf.Abs(Player.transform.position.x - leftSea.transform.position.x);
		spacing = Vector3.Distance (currSea.transform.position, topSea.transform.position);
	}

	/*Checks whether any of the 4 water gameObjects need to be moved*/
	void Update ()
	{
		leftSea = checkIfCurr (leftSea);
		topSea = checkIfCurr (topSea);
		diagSea = checkIfCurr (diagSea);

		//Updates position of water gameObjects if need be
		if(!hasUpdated)
			updatePos ();
	}

	/*Checks if parameter sea is the sea the player is currently on top of*/
	GameObject checkIfCurr(GameObject sea){

		if (Mathf.Abs(Player.transform.position.x - sea.transform.position.x) < distance*.3
			&& Mathf.Abs(Player.transform.position.z - sea.transform.position.z) < distance*.3) {
			GameObject tempSea = currSea;
			currSea = sea;
			hasUpdated = false;
			return tempSea;
		}

		return sea;
	}

	/*Corrects the positions of the sea gameObjects*/
	void updatePos(){
		//moves left, above and diagonal sea to correct positions
		if (Player.transform.position.x < currSea.transform.position.x
			&& Player.transform.position.z > currSea.transform.position.z) {
			Vector3 newPos = new Vector3 (currSea.transform.position.x - spacing, Y_POS_SEA, currSea.transform.position.z); 
			leftSea.transform.position = newPos;

			newPos = new Vector3 (currSea.transform.position.x, Y_POS_SEA, currSea.transform.position.z + spacing); 
			topSea.transform.position = newPos;

			newPos = new Vector3 (currSea.transform.position.x - spacing, Y_POS_SEA, currSea.transform.position.z + spacing); 
			diagSea.transform.position = newPos;
			hasUpdated = true;
		}
		//move the left sea to correct position
		else if (Player.transform.position.x < currSea.transform.position.x) {
			Vector3 newPos = new Vector3 (currSea.transform.position.x - spacing, Y_POS_SEA, currSea.transform.position.z); 
			leftSea.transform.position = newPos;
		}
		//move the above sea to correct position
		else if (Player.transform.position.z > currSea.transform.position.z) {
			Vector3 newPos = new Vector3 (currSea.transform.position.x, Y_POS_SEA, currSea.transform.position.z + spacing); 
			topSea.transform.position = newPos;
		}
	}
}
