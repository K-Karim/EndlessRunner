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
	public GameObject optionsPage;
	public GameObject[] demoObject;
	public GameObject[] particle;
	public static int currentParticleIndex;


	private KeyCode[] keyCodes = {
		KeyCode.Alpha2,
		KeyCode.Alpha3,
		KeyCode.Alpha4,
	};



	// Use this for initialization
	void Start ()
	{
		
		print ("Start menu manager");
		playButton.onClick.AddListener(PlayGame);
		if (isReset) {
			print ("is Reset");
			hideHomeMenu ();

		} else {
			currentParticleIndex = particle.Length;
		}
		if (currentParticleIndex != particle.Length) {
			particle [currentParticleIndex].gameObject.SetActive (true);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Start game
		if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))) {
			PlayGame ();
		}
			

		// Menu button display 
		if (homeMenu.gameObject.activeSelf && Input.GetKeyDown (KeyCode.I)) {
			hideHomeMenu ();
			instructionsPage.SetActive (true);
			SetDemos (true);
		}

		if (homeMenu.gameObject.activeSelf && Input.GetKeyDown(KeyCode.O) ) {
			hideHomeMenu ();
			optionsPage.SetActive (true);
		}

		if (optionsPage.gameObject.activeSelf) {
			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				clearParticle ();
				currentParticleIndex = particle.Length;

			} else {

				for (int i = 0; i < keyCodes.Length; i++) {
					if (Input.GetKeyDown (keyCodes [i])) {
						clearParticle ();
						particle [i].gameObject.SetActive (true);
						currentParticleIndex = i;
					}
				}

			}
		}












		// Escape to home menu
		if (Input.GetKeyDown(KeyCode.X) ) {
			showHomeMenu ();
			closeInstruction ();
			optionsPage.SetActive (false);
		}


	}

	public void PlayGame() {
		print ("Play game");
		playerScript.direction = Vector3.forward;
		playerScript.FixedUpdate ();
		hideHomeMenu ();
		closeInstruction ();
		optionsPage.SetActive (false);
		this.gameObject.SetActive (false);
	}

	public void hideHomeMenu() {
		homeMenu.gameObject.SetActive (false);
	}

	public void showHomeMenu() {
		homeMenu.gameObject.SetActive (true);
	}

	public void closeInstruction() {
		instructionsPage.SetActive (false);
		SetDemos (false);
	}

	public void SetDemos(bool status) {
		for (int i = 0; i < demoObject.Length; i++) {
			demoObject [i].gameObject.SetActive (status);
		}
	}

	public void clearParticle() {
		for (int i = 0; i < particle.Length; i++) {
			particle [i].gameObject.SetActive (false);
		}
	}
}

