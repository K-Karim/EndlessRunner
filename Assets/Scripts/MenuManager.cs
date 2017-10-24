/*
 * Graphics and Interaction (COMP30019) 
 * Project 2: Endless Runner
 * Team: Karim Khairat, Duy (Daniel) Vu, and Brody Taylor
 * 
 * Manage all game menu of including Playgame, Instruction and Options chosen menu
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

	public PlayerScript playerScript;

	/* Home menu buttons */
	public Button playButton;
	public Button instructionButton;
	public Button optionsButton;

	public GameObject homeMenu;
	public GameObject instructionsPage;
	public GameObject optionsPage;

	public GameObject[] demoObject;			// Demo objects for instruction page
	public GameObject[] particle;			// Particle for option Page

	public static bool isRetry = false;		// Indicator for Retry game
	public static int currentParticleIndex;	// Current chosen particle effect
	private static bool isDefaultParticle = true;

	/* Key to choose player's appearance options */
	private KeyCode[] OptionsKeyCodes = {
		KeyCode.Alpha2,
		KeyCode.Alpha3,
		KeyCode.Alpha4,
	};


	/* Set up menu view: 
	- [Home menu] Show home menu and reset all options
	- [Retry] Hide home menu and keep current option for user
	*/
	void Start ()
	{
		if (isRetry) {
			hideHomeMenu ();
			if (!isDefaultParticle) {
				particle [currentParticleIndex].gameObject.SetActive (true);
			}

		} else {
			// default particle option is no effect
			currentParticleIndex = particle.Length;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Start game
		if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))) {
			PlayGame ();
		}
			
		// Instruction page display 
		if (homeMenu.gameObject.activeSelf && Input.GetKeyDown (KeyCode.I)) {
			hideHomeMenu ();
			instructionsPage.SetActive (true);
			SetDemos (true);
		}

		// Option page display 
		if (homeMenu.gameObject.activeSelf && Input.GetKeyDown(KeyCode.O) ) {
			hideHomeMenu ();
			optionsPage.SetActive (true);
		}

		// Choose option for player's appearance
		if (optionsPage.gameObject.activeSelf) {
			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				clearParticle ();
				isDefaultParticle = true;

			} else {
				for (int i = 0; i < OptionsKeyCodes.Length; i++) {
					if (Input.GetKeyDown (OptionsKeyCodes [i])) {
						clearParticle ();
						particle [i].gameObject.SetActive (true);
						currentParticleIndex = i;
						isDefaultParticle = false;
					}
				}
			}
		}

		// Exit to home menu
		if (Input.GetKeyDown(KeyCode.X) ) {
			showHomeMenu ();
			closeInstruction ();
			optionsPage.SetActive (false);
		}
	}

	/* Start playing game action */
	public void PlayGame() {
		// Move player
		playerScript.direction = Vector3.forward;
		playerScript.FixedUpdate ();

		// Hide all menus
		hideHomeMenu ();
		closeInstruction ();
		optionsPage.SetActive (false);
		this.gameObject.SetActive (false);
	}

	/* Hide home menu */
	public void hideHomeMenu() {
		homeMenu.gameObject.SetActive (false);
	}

	/* Show home menu */
	public void showHomeMenu() {
		homeMenu.gameObject.SetActive (true);
	}

	/* Close instruction page and demos */
	public void closeInstruction() {
		instructionsPage.SetActive (false);
		SetDemos (false);
	}

	/* Set visibility status of demos */
	public void SetDemos(bool status) {
		for (int i = 0; i < demoObject.Length; i++) {
			demoObject [i].gameObject.SetActive (status);
		}
	}

	/* Disable all particles system */
	public void clearParticle() {
		for (int i = 0; i < particle.Length; i++) {
			particle [i].gameObject.SetActive (false);
		}
	}
}

