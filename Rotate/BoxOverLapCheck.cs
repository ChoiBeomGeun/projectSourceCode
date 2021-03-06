﻿/******************************************************************************/
/*!
\file   BoxOverLapCheck.cs
\author BeomGeun Choi
\brief
This file is for the checking one of the lose condition : Box falls over the player
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoxOverLapCheck : MonoBehaviour 
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D myCollider = GetComponent<Collider2D>();
        int numColliders = 5;
        Collider2D[] colliders = new Collider2D[numColliders];
        ContactFilter2D contactFilter = new ContactFilter2D();

        int colliderCount = myCollider.OverlapCollider(contactFilter, colliders);

		// Only check between Player and the box, attach box
        if (collision.collider.tag == "Box" || collision.collider.tag == "AttachBox")
        {
			//Check box is falling or not
            if (collision.collider.GetComponent<Rigidbody2D>().velocity.y < 0F)
            {
                foreach (ContactPoint2D contact in collision.contacts)
                {
                    //If the box is on the  head of player, restart the game
                    if (contact.normal.normalized == new Vector2(0f, -1f))
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }
                }
                   
            }
        }
    }

 
}
