/*
 * Graphics and Interaction (COMP30019) Project 2
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
	private double distance;
	private float spacing;
	private bool hasUpdated = true;
	private float yPos;
	
	void Start ()
	{
		currSea = seas [0];
		leftSea = seas [1];
		topSea = seas [2];
		diagSea = seas [3];
		yPos = currSea.transform.position.y;
		distance = Mathf.Abs(Player.transform.position.x - leftSea.transform.position.x);
		spacing = Vector3.Distance (currSea.transform.position, topSea.transform.position);
	}
	
	void Update ()
	{
		leftSea = checkIfCurr (leftSea);
		topSea = checkIfCurr (topSea);
		diagSea = checkIfCurr (diagSea);

		if(!hasUpdated)
			updatePos ();
	}

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

	void updatePos(){
		if (Player.transform.position.x < currSea.transform.position.x
			&& Player.transform.position.z > currSea.transform.position.z) {
			Vector3 newPos = new Vector3 (currSea.transform.position.x - spacing, yPos, currSea.transform.position.z); 
			leftSea.transform.position = newPos;

			newPos = new Vector3 (currSea.transform.position.x, yPos, currSea.transform.position.z + spacing); 
			topSea.transform.position = newPos;

			newPos = new Vector3 (currSea.transform.position.x - spacing, yPos, currSea.transform.position.z + spacing); 
			diagSea.transform.position = newPos;
			hasUpdated = true;
		} else if (Player.transform.position.x < currSea.transform.position.x) {
			Vector3 newPos = new Vector3 (currSea.transform.position.x - spacing, yPos, currSea.transform.position.z); 
			leftSea.transform.position = newPos;
		} else if (Player.transform.position.z > currSea.transform.position.z) {
			Vector3 newPos = new Vector3 (currSea.transform.position.x, yPos, currSea.transform.position.z + spacing); 
			topSea.transform.position = newPos;
		}
	}
}
