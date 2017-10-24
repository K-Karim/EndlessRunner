﻿/*
 * Graphics and Interaction (COMP30019)
 * Project 2: Endless Runner
 * Team: Karim Khairat, Duy (Daniel) Vu, and Brody Taylor
 * 
 * Camera follows a third-person approach, where it follows the user, 
 * with a locked y axis, until the user falls off the platform, at which point it locks the remaining axes
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject player;
    private PlayerScript ps;
    private Vector3 offset;
    private float ydist;

    void Start()
    {
        ps = player.GetComponent<PlayerScript>();
        offset = transform.position - player.transform.position;

        //we initialise ydist as this to keep the y camera position static. 
        //So the camera doesn't follow the ball into the abyss
        ydist = player.transform.position.y;
    }

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
