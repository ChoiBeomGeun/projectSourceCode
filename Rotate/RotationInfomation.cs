/******************************************************************************/
/*!
\file   RotationInfomation.cs
\author BeomGeun Choi
\brief
This file is for rotating the world
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationInfomation : MonoBehaviour
{
    public GameObject mOpivot;
    public GameObject mOplayer;
	
    public int mIrotationSpeed = 5;
    public int mIsetRotationValue = 0;
    public int mIrotationIndicator = 0;
	
    public bool mBisRotating = false;
    public bool mBisRightTrigger = false;

    void FixedUpdate()
    {
        //Rotate the objects
        if (mBisRotating)
        {
            mOplayer.GetComponent<Rigidbody2D>().Sleep();
            mIrotationIndicator++;
            GameObject[] gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
            foreach (GameObject go in gos)
            {
                if (go.layer == 0)
                {
                    if (!mBisRightTrigger)               
                    {
                        go.transform.RotateAround(mOplayer.transform.localPosition, Vector3.forward, mIrotationSpeed);

                    }
                    else
                    {
                        go.transform.RotateAround(mOplayer.transform.localPosition, Vector3.back, mIrotationSpeed);                          
                    }
                }
            }
            //Check whether the Rotation is completed
            if (mIsetRotationValue == mIrotationIndicator)
            {
                mOplayer.GetComponent<Rigidbody2D>().WakeUp();          
                mIrotationIndicator = 0;
                mOplayer.transform.rotation = Quaternion.Euler(0, 0, 0);
        
                mBisRotating = false;
            }
        }
    }
}
