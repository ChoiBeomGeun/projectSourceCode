/******************************************************************************/
/*!
\file   MeteorSpawner.cs
\author BeomGeun Choi
\brief
Meteor Spawner Logic in top view game
*/
/******************************************************************************/

using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : SpawnerClass
{    
	[System.Serializable]
    public struct MinMax
    {
        public float Min;
        public float Max;
    }
	
    public GameObject []mOislandLines;
    public GameObject mOmeteorPrefab;
    public GameObject mOcoinPrefab;
	
    public float mFmeteorSpawnOffsetX;

    public MinMax mMMtimeToSpawnForMeteor;
    public MinMax mMMtimeToSpawnForCoin;


    GameObject mOmeteor;
    GameObject mOcoin;
	
    bool mBisMeteorSpawnDelayOn= true;
    bool mBisCoinSpawnDelayOn = true;

    // Use this for initialization
    void Start () 
	{
        mOislandLines = GameObject.FindGameObjectsWithTag("Island");
    }
   
    void Update () 
	{
        if (mBisMeteorSpawnDelayOn)
        {
            CreateMeteor();
        }

        if (mBisMeteorSpawnDelayOn)
        {
            CreateCoinObject();
        }
    }
	//Spawn Meteor at random line
	void CreateMeteor()
    {
        mOmeteor = (GameObject)Instantiate(mOmeteorPrefab);
        int SelectedLine = UnityEngine.Random.Range(0, mOislandLines.Length);
        mOmeteor.transform.position = new Vector3(
            mOislandLines[SelectedLine].transform.position.x + mFmeteorSpawnOffsetX,
            mOislandLines[SelectedLine].transform.position.y,
            mOislandLines[SelectedLine].transform.position.z);
        mBisMeteorSpawnDelayOn = false;
        StartCoroutine("GiveMeteorDelay",UnityEngine.Random.Range(mMMtimeToSpawnForMeteor.Min, mMMtimeToSpawnForMeteor.Max));
    }
	//Spawn Coin at random line
    void CreateCoinObject()
    {
        mOcoin = (GameObject)Instantiate(mOcoinPrefab);
        int SelectedLine = UnityEngine.Random.Range(0, mOislandLines.Length);
        mOcoin.transform.position = new Vector3(
            mOislandLines[SelectedLine].transform.position.x + mFmeteorSpawnOffsetX,
            mOislandLines[SelectedLine].transform.position.y+1,
            mOislandLines[SelectedLine].transform.position.z);
        mBisCoinSpawnDelayOn = false;
        StartCoroutine("GiveCoinDelay", UnityEngine.Random.Range(mMMtimeToSpawnForCoin.Min, mMMtimeToSpawnForCoin.Max));
    }
    //Coroutine for Spawn Delay
    IEnumerator GiveCoinDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        mBisCoinSpawnDelayOn = true;
    }
    IEnumerator GiveMeteorDelay(float delay )
    {
        yield return new WaitForSeconds(delay);
        mBisMeteorSpawnDelayOn = true;
    }
}
