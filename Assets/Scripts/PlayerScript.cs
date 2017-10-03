

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

	//Starting Speed will be set in unity!
	public float speed;
	private Vector3 direction;
	private int Score=0;
    private static int hScore=0;
	public Text ScoreTxt;
    public Text HScoreTxt;

    private bool hasleveled = true;
    private int increaseS= 50;
    private int lvlscore = 0;

    private bool Alive = true;

    public GameObject SlowPS;
    public GameObject FastPS;
    public GameObject Plus5PS;
    public GameObject Plus10PS;
    public GameObject Plus50PS;

    public GameObject ResetPrompt;
    // Use this for initialization
    void Start () {
		//doesn't move until user presses/ picks location
		Score = 0;
		direction = Vector3.zero;
        HScoreTxt.text = hScore.ToString();

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
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

            //if user hits levelup threshold, add 1 to speed.
            if (lvlscore % increaseS == 0 && lvlscore != 0)
            {
                //ensures that +1 to speed is only added once per level increase!
                if (hasleveled)
                {
                    speed++;
                    lvlscore = 0;
                    hasleveled = !hasleveled;
                }
            }
            else
            {
                hasleveled = true;
            }
        }
        else
        {
            ResetPrompt.SetActive(true);
            if (Input.GetKeyDown(KeyCode.KeypadEnter) ||Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

	void FixedUpdate(){

		float moved = speed * Time.deltaTime;
		//Speed and distance moved :)                                                                                                                           
		transform.Translate(direction * moved);
	}
    
    //Once you exit the tile, you get a point! 
    void OnTriggerExit(Collider tile)
	{
		if(tile.tag == "Tile"){
			Score++;
            lvlscore++;
			ScoreTxt.text = Score.ToString ();
            if(hScore == 0 || Score> hScore)
            {
                hScore = Score;

            }
            HScoreTxt.text = hScore.ToString();

            RaycastHit raycasthit;
            Ray ray = new Ray(transform.position, Vector3.down);
            if (!Physics.Raycast(ray, out raycasthit))
            {
                Alive = false;
            }
		}
   
   	}
    private void OnTriggerEnter(Collider Pickup)
    {
        if (Pickup.tag == "Slow")
        {
            speed--;
            Pickup.gameObject.SetActive(false);
            Instantiate(SlowPS, transform.position, Quaternion.identity);
        }
        else if (Pickup.tag == "Fast")
        {
            speed++;
            Pickup.gameObject.SetActive(false);
            Instantiate(FastPS, transform.position, Quaternion.identity);


        }
        else if (Pickup.tag == "+5")
        {
            Score += 5;
            Pickup.gameObject.SetActive(false);
            Instantiate(Plus5PS, transform.position, Quaternion.identity);


        }
        else if (Pickup.tag == "+10")
        {
            Score += 10;
            Pickup.gameObject.SetActive(false);
            Instantiate(Plus10PS, transform.position, Quaternion.identity);


        }
        else if (Pickup.tag == "+50")
        {
            Score += 50;
            Pickup.gameObject.SetActive(false);
            Instantiate(Plus50PS, transform.position, Quaternion.identity);

        }
    }
    public bool getAlive()
    {
        return Alive;
    }
}
