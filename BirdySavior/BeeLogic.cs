using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class BeeLogic : MonoBehaviour
{
    
    public Transform mTspawnPoint;
    public float mFspeedOfBee;
    public float mFattackDelayOfBee;
	public float mFshakepowerOfBee = 1.5f;
	//Bee's target,it is spawn point
	Transform mTtarget;
	
    float mFtimeToAttack;
	
    bool mBisMoving;
    bool mBisShakable = true;
    // Use this for initialization
    void Start () 
	{
        GetComponent<Collider>().enabled = false;
		//Bee will find the earth and will attack
		mTtarget = GameObject.FindGameObjectWithTag("Ground").transform;
        mFtimeToAttack = mFattackDelayOfBee;
    }
	
	// Update is called once per frame
	void Update ()
	{
		//Moving toward the spawn point
        Vector3 relativePos = mTtarget.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation;
        transform.position = Vector3.MoveTowards(transform.position,mTspawnPoint.position,mFspeedOfBee * Time.deltaTime);
		//Bee arrives the spawn point and will attack the earth
        if (mBisMoving == false)
        {
			//When Bee is ready for the attack, it will shake itself.
            if (mFtimeToAttack < 1 && mBisShakable)
            {
                transform.localPosition = (Vector3)Random.insideUnitCircle * mFshakepowerOfBee + mTspawnPoint.position;
            }
            if (mFtimeToAttack > 0)
            {
                mFtimeToAttack -= Time.deltaTime;
            }
            else
            {
                mBisShakable = false;
                mFtimeToAttack = mFattackDelayOfBee;
                mTspawnPoint = mTtarget;
            }
        }
		//Condition for the checking bee arrives at the spawn point
        if (transform.position == mTspawnPoint.position)
        {
            GetComponent<Collider>().enabled = true;
            mBisMoving = false;
        }
        else
		{		
			mBisMoving = true;
		}
   
    }
}
