/******************************************************************************/
/*!
\file   PlayerInputManage.cs
\author BeomGeun Choi
\brief
This file is for controling user's input if the state is paused or rotating
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManage : MonoBehaviour
{
    GameObject mOpivot;
    GameObject mOgameLogic;
	GameObject mOplayer;
    GameObject mOcamera;

    // Use this for initialization
    void Start ()
	{
		//todo
		//Change "Find" Method to assign Public Object.
        mOpivot = GameObject.Find("Pivot");
        mOgameLogic = GameObject.Find("GAMELOGIC");
        mOplayer = GameObject.FindWithTag("Player");
        mOcamera = GameObject.Find("Main Camera");

    }

    // Update is called once per frame
    void Update()
    {
        if (mOpivot.GetComponent<RotationInfomation>().mBisRotating
		|| mOgameLogic.GetComponent<PauseManage>().mBisPaused)
        {
            mOplayer.GetComponent<CapsuleCollider2D>().isTrigger = true;
            mOplayer.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            mOplayer.GetComponent<APCharacterController>().m_basic.m_maxFallSpeed = 0;
            mOplayer.GetComponent<APCharacterController>().enabled = false;
            mOcamera.GetComponent<SmoothFollow>().enabled = false;
        }
        else
        {
            mOplayer.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            mOplayer.GetComponent<APCharacterController>().enabled = true;
            mOplayer.GetComponent<APCharacterController>().m_basic.m_maxFallSpeed = 60;
            mOplayer.GetComponent<CapsuleCollider2D>().isTrigger = false;
            mOcamera.GetComponent<SmoothFollow>().enabled = true;

        }
    }
}
