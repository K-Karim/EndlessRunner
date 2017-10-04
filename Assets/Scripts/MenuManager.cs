using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

	public PlayerScript playerScript;
	public Button playButton;
	public Button instructionButton;
	public Button optionsButton;
	public GameObject homeMenu;
	public static bool isReset = false;

	// Use this for initialization
	void Start ()
	{
		print ("Start menu manager");
		playButton.onClick.AddListener(PlayGame);
		if (isReset) {
			print ("is Reset");
			hideHomeMenu ();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
			PlayGame ();
		}
	}

	public void PlayGame() {
		print ("Play game");
		playerScript.direction = Vector3.forward;
		playerScript.FixedUpdate ();
		hideHomeMenu ();
	}

	public void hideHomeMenu() {
		print ("Hide home menu");
		homeMenu.gameObject.SetActive (false);
	}
}

