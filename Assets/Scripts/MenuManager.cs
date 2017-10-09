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
	public GameObject fireTraling; 
	public GameObject instructionsPage;

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
		if (Input.GetKeyDown(KeyCode.Plus) ) {
			fireTraling.SetActive (false);
		}
		if (Input.GetKeyDown(KeyCode.Minus) ) {
			fireTraling.SetActive (true);
		}
		if (Input.GetKeyDown(KeyCode.I) ) {
			hideHomeMenu ();
			instructionsPage.SetActive (true);
		}
		if (Input.GetKeyDown(KeyCode.Escape) ) {
			showHomeMenu ();
			instructionsPage.SetActive (false);
		}

	}

	public void PlayGame() {
		print ("Play game");
		playerScript.direction = Vector3.forward;
		playerScript.FixedUpdate ();
		hideHomeMenu ();
	}

	public void hideHomeMenu() {
		homeMenu.gameObject.SetActive (false);
	}

	public void showHomeMenu() {
		homeMenu.gameObject.SetActive (true);
	}

	public void closeInstruction() {
		instructionsPage.SetActive (false);
		showHomeMenu ();
	}
}

