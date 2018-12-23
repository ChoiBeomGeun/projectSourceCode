/******************************************************************************/
/*!
\file   PauseManage.cs
\author BeomGeun Choi
\brief
This file is for handling in Pause State
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManage : MonoBehaviour 
{
    public GameObject mOpauseUI;
	public bool mBisPaused = false;
	
    GameObject mOmainUI;
    GameObject mOplayer;
	// Use this for initialization
	void Start () 
	{
        mOmainUI = GameObject.FindGameObjectWithTag("MainUI");
        mOplayer = GameObject.FindWithTag("Player");

    }
	
	// Update is called once per frame
	void Update () 
	{
        if (Input.GetKeyUp("escape"))
        {
            PauseAdjust();
        }
    }

    public void PauseAdjust()
    {  
			//Condition For game state is not paused
            if (!mOpauseUI && !mBisPaused)
            {
				if(!mOmainUI)
				{
					mOmainUI = GameObject.FindGameObjectWithTag("MainUI");
				}
				mOpauseUI = MonoBehaviour.Instantiate(Resources.Load("Prefabs/PauseUI") as GameObject);
                mBisPaused = true;
				mOmainUI.SetActive(false);
				mOplayer.GetComponent<APCharacterController>().enabled = false;
			}
            else if (mOpauseUI)
            {
                Destroy(mOpauseUI);
                mOmainUI.SetActive(true);
                mBisPaused = false;
				mOplayer.GetComponent<APCharacterController>().enabled = true;
			}
    }
}


