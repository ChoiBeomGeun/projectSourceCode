/******************************************************************************/
/*!
\file   TriggerLogic.cs
\author BeomGeun Choi
\brief
This file is for the Triggers 90,180 degrees
*/
/******************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerLogic : MonoBehaviour
{
    const int mI_TRIGGER_INS_DISTANCE = 12;
    const int mI_TRIGGER_INS_Y_OFFSET = 7;
  
    public AudioSource m_TriggerSound;
    //Delay of Triggers
    public float SetDelay = 5;
    //Angle To Rotate
    public float mFangleToRotate = 90f;
	
    public bool mBisRightTrigger = false;
    public bool mBtriggerDelayOn = false;

    bool mBisTriggerOn = true;
	
    GameObject mOpivot;
    GameObject mOplayer;
    GameObject mOtriggerIns;

    void Start () {
        Destroy(GetComponent<Rigidbody2D>());
        GetComponent<BoxCollider2D>().isTrigger = true;
        mOpivot = GameObject.FindGameObjectWithTag("Pivot");
        mOplayer = GameObject.Find("Player");
        SetInstrucionOfTrigger();

    }

    private void FixedUpdate()
    {
        //If player's position is nearby the trigger, Active the Instruction of Trigger Direction
        float dist = Vector3.Distance(mOplayer.GetComponent<Transform>().position, transform.position);

        if (dist < mI_TRIGGER_INS_DISTANCE)
        {
            mOtriggerIns.SetActive(true);
            mOtriggerIns.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -transform.rotation.z);
            mOtriggerIns.transform.position = transform.position;
            mOtriggerIns.transform.position += new Vector3(0, mI_TRIGGER_INS_Y_OFFSET, 0);
        }
        else
        {
            mOtriggerIns.SetActive(false);
        }

        //Init Trigger Delay
        if(!mBisTriggerOn && mBtriggerDelayOn)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            mBisTriggerOn = true;
            mBtriggerDelayOn = false;
        }
    }
    //Coroutine for TriggerDealy
    IEnumerator GiveTriggerDelay()
    {
        yield return new WaitForSeconds(SetDelay);
        mBtriggerDelayOn = true;
    }
    //Collision detection between player and this trigger
    void OnTriggerEnter2D(Collider2D other)
    {

        RotationInfomation rotationInfomation = mOpivot.GetComponent<RotationInfomation>();
        if (other.GetComponent<Collider2D>().tag == "Player" && mBisTriggerOn)
        {
            m_TriggerSound.Play();
            mOpivot.transform.localPosition = this.transform.localPosition;
            StartCoroutine("GiveTriggerDelay");

            rotationInfomation.mIsetRotationValue = (int) (mFangleToRotate * 0.2);
            rotationInfomation.mBisRotating = true;
            rotationInfomation.mBisRightTrigger = mBisRightTrigger;

            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.3f);
            GetComponent<BoxCollider2D>().isTrigger = true;
            mBisTriggerOn = false;
        }

    }
    // Setting Function for Instrucion Of Trigger
    void SetInstrucionOfTrigger()
    {
        if (mFangleToRotate == 90)
        {
            if (mBisRightTrigger)
            {
                mOtriggerIns = MonoBehaviour.Instantiate(Resources.Load("Prefabs/90TriggerInsRight") as GameObject);
            }
            else
            {
                mOtriggerIns = MonoBehaviour.Instantiate(Resources.Load("Prefabs/90TriggerLefttIns") as GameObject);
            }
        }
        else
        {
            mOtriggerIns = MonoBehaviour.Instantiate(Resources.Load("Prefabs/180TriggerIns") as GameObject);
        }

        mOtriggerIns.GetComponent<Transform>().SetParent(transform);
        mOtriggerIns.transform.position = transform.position;
        mOtriggerIns.transform.position += new Vector3(0, mI_TRIGGER_INS_Y_OFFSET, 0);


        mOtriggerIns.SetActive(false);
    }


}
