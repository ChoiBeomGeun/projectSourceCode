/******************************************************************************/
/*!
\file   CoinLogic.cs
\author BeomGeun Choi
\brief
Coin Logic in all three game.
It will be change as the game type
*/
/******************************************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinLogic : MonoBehaviour 
{
    GameObject mOgameLogic;
	
    public Vector3 mVec3priorVel;
	
    public float mFcoinTimer = 10;
	
    SpawnerClass.SpawnerType mSTcurrentSpawnType;
	
    void Awake()
    {
        mOgameLogic = GameObject.FindGameObjectWithTag("GameLogic");
        mSTcurrentSpawnType = GameObject.FindGameObjectWithTag("Spawner").GetComponent<SpawnerClass>().mSpawnerType;
		//Adjust Coin as the gametype (side,top,first)
        if (mSTcurrentSpawnType == SpawnerClass.SpawnerType.Top)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            transform.Rotate( new Vector3(0, 90f, 0));
            mFcoinTimer = 20;
        }
        if (mSTcurrentSpawnType == SpawnerClass.SpawnerType.First)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            transform.Rotate(new Vector3(0, 90f, 0));
            GetComponent<SphereCollider>().center = new Vector3(0, 0, 0);
        }
        if (mSTcurrentSpawnType == SpawnerClass.SpawnerType.Side)
        {
            transform.GetComponent<SphereCollider>().radius = 0.001f;
        }
        InvokeDestoryCoin();
    }
    // Update is called once per frame
    void Update ()
	{
        if (!mOgameLogic.GetComponentsInChildren<SceneSwitchLogic>()[0].mBisPaused)
        {
            if (mSTcurrentSpawnType == SpawnerClass.SpawnerType.Top)
            {
                GetComponent<Rigidbody>().AddForce(new Vector3(-5, 0, 0));
            }
        }
    }
	//Invoke function for Destory Coin
    public void InvokeDestoryCoin()
    {
        Invoke("DestoryCoin", mFcoinTimer);
    }
	// function for Cancle the "InvokeDestoryCoin"
    public void CancelInvokeDestory()
    {
        CancelInvoke("DestoryCoin");

    }
	// function for saving priorVelocity of Coin
    public void SavePriorVelocity()
    {
        mVec3priorVel = GetComponent<Rigidbody>().velocity;

    }
	// function for Deleting Coin,it will be used with Invoke function 
    void DestoryCoin()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<AudioSource>().Play();
            Destroy(gameObject,0.1f);
            GameObject.FindGameObjectWithTag("ScoreBoard").GetComponent<SumScoreExample>().AddCoins(1);

        }
    }


}
