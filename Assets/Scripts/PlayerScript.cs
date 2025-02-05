﻿/*
 * Graphics and Interaction (COMP30019) 
 * Project 2: Endless Runner
 * Team: Karim Khairat, Duy (Daniel) Vu, and Brody Taylor
 * 
 * Controler action of player and interaction with game objects
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

	public float speed;		//Starting Speed will be set in unity!
	public Vector3 direction;

	/* Game score */
	private int Score=0;			// Current score
    private static int hScore=0;	// Highscore

	/* Score text */
	public Text ScoreTxt;
    public Text HScoreTxt;
	public Text PowerupTxt;
	public Text FinalScoreTxt;
	public Text FinalHScoreTxt;

    /*level tracking*/
    private bool hasleveled = true;
    private int increaseS= 50;
    private int lvlscore = 0;

	/* Indicator of player's alive */
    private bool Alive = true;

	/* Power-ups*/
    public GameObject SlowPS;
    public GameObject FastPS;
    public GameObject Plus5PS;
    public GameObject Plus10PS;
    public GameObject Plus50PS;

    public GameObject ResetPrompt;
	public Text newHS;
	public Animator gameOverAnimator;
	public MenuManager menuManager;
	public AudioSource pickup;
    
	/* Reset score and retain highscore */
    void Start () {
		Score = 0;
		HScoreTxt.text = hScore.ToString();

		direction = Vector3.zero;	//doesn't move until user presses/ picks location
	}

    /* Update player position with corresponding action */
    void Update () {
        // Quit application
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

		// Move player if still alive
        if (Alive) {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                direction = Vector3.forward;
            } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                direction = Vector3.left;

            } else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                direction = Vector3.right;

            } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                direction = Vector3.down;
            }

            //space = JUMP, however causes complications atm.
            else if (Input.GetKeyDown(KeyCode.Space)) {
                //direction = Vector3.up;
			}

            // If user hits levelup threshold, add 1 to speed.
            if (lvlscore % increaseS == 0 && lvlscore != 0) {
                //ensures that +1 to speed is only added once per level increase!
                if (hasleveled) {
                    speed++;
                    lvlscore = 0;
                    hasleveled = !hasleveled;
                }
            } else {
                hasleveled = true;
            }

		// End game when player not alive
        } else {
			GameOver ();
		}
    }

	/* Game over action */
	void GameOver() {
		// Set animation 
		gameOverAnimator.SetTrigger ("GameOver");

		// Set scores
		FinalScoreTxt.text = Score.ToString ();
		FinalHScoreTxt.text = hScore.ToString ();

		// Receive follow up action
		if (Input.GetKeyDown (KeyCode.KeypadEnter) || Input.GetKeyDown (KeyCode.Return)) {
			RetryGame ();
		}
		if (Input.GetKeyDown (KeyCode.H)) {
			GoHome ();
		}
	}

	/* Retry game */
	public void RetryGame() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		MenuManager.isRetry = true;
	}

	/* Stop game and return to home main menu */
	public void GoHome() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		MenuManager.isRetry = false;
	}

	/* Start the game, move the player */
	public void startGame() {
		direction = Vector3.forward;
	}

	public void FixedUpdate(){
		//Speed and distance moved :)                                                                                                                           
		float moved = speed * Time.deltaTime;
		transform.Translate(direction * moved);
	}
    
    /* Once you exit the tile, you get a point! */
    void OnTriggerExit(Collider tile)
	{
		if(tile.tag == "Tile"){
			Score++;
            lvlscore++;
			ScoreTxt.text = Score.ToString ();
            if(hScore == 0 || Score> hScore)
            {
                hScore = Score;
				newHS.gameObject.SetActive (true);

            }
            HScoreTxt.text = hScore.ToString();

		}
        //check if there's a tile below you, if not you lose :(
		RaycastHit raycasthit;
		Ray ray = new Ray(transform.position, Vector3.down);
		if (!Physics.Raycast(ray, out raycasthit))
		{
			Alive = false;
		}
	}

	/* Effects for powerup pick up */
    private void OnTriggerEnter(Collider Pickup)
    {
		// Slow speed
        if (Pickup.tag == "Slow")
        {
            speed--;
            Pickup.gameObject.SetActive(false);
            Instantiate(SlowPS, transform.position, Quaternion.identity);
            //display powerup text
            PowerupTxt.text = "Slow!".ToString();
            //make powerup disappear
            StartCoroutine(disappear());
            //play powerup sound
            pickup.Play();
        }

		// Increase speed 
        else if (Pickup.tag == "Fast")
        {
            speed++;
            Pickup.gameObject.SetActive(false);
            Instantiate(FastPS, transform.position, Quaternion.identity);
            //display powerup text
            PowerupTxt.text = "Fast!".ToString();
            //make powerup disappear
            StartCoroutine(disappear());
            //play powerup sound
            pickup.Play();
        }

        // +5 points!
        else if (Pickup.tag == "+5")
        {
            Score += 5;
            Pickup.gameObject.SetActive(false);
            Instantiate(Plus5PS, transform.position, Quaternion.identity);
            //display powerup text
            PowerupTxt.text = "+5 Points!".ToString();
            //make powerup disappear
            StartCoroutine(disappear());
            //play powerup sound
            pickup.Play();
        }
        // +10 points!
        else if (Pickup.tag == "+10")
        {
            Score += 10;
            Pickup.gameObject.SetActive(false);
            Instantiate(Plus10PS, transform.position, Quaternion.identity);
            //display powerup text
            PowerupTxt.text = "+10 Points!".ToString();
            //make powerup disappear
            StartCoroutine(disappear());
            //play powerup sound
            pickup.Play();

        }
        // +50 points!
        else if (Pickup.tag == "+50")
        {
            Score += 50;
            Pickup.gameObject.SetActive(false);
            Instantiate(Plus50PS, transform.position, Quaternion.identity);
            //display powerup text
			PowerupTxt.text = "+50 Points!".ToString();
            //make powerup disappear
			StartCoroutine(disappear ());
            //play powerup sound
			pickup.Play ();
		}
    }

	/* Getter for player alive status */
    public bool getAlive()
    {
        return Alive;
    }

	/* Powerup text disappear after time */
	IEnumerator disappear(){
		yield return new WaitForSeconds(1);
		PowerupTxt.text = "".ToString ();
	}

}
