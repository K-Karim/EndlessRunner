using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject player;
    private PlayerScript ps;
    private Vector3 offset;
    private float ydist;
    // Use this for initialization
    void Start()
    {
        ps = player.GetComponent<PlayerScript>();
        offset = transform.position - player.transform.position;
        //we initialise ydist as this to keep the y camera position static. 
        //So the camera doesn't follow the ball into the abyss
        ydist = player.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

        if (ps.getAlive())
        {
            transform.position = new Vector3(player.transform.position.x, ydist, player.transform.position.z) + offset;
            //Lock camera rotation
            transform.rotation = Quaternion.Euler(new Vector3(40, 0, 0));
        }
    }
}
