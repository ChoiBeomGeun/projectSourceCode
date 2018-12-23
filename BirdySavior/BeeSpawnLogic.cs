/******************************************************************************/
/*!
\file   BeeSpawnLogic.cs
\author BeomGeun Choi
\brief
Bee Spawn Logic in First View Game
*/
/******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpawnerClass))]
public class BeeSpawnLogic : SpawnerClass
{
    [System.Serializable]
    public struct MinMax
    {
        public float Min;
        public float Max;
    }

    public GameObject mObeePrefab;
    public GameObject mOcoinPrefab;
	
    public float mFspeedOfBee;
    public float mFattackDelayOfBee;
	
    public MinMax mMMtimeToSpawnForBee;
    public MinMax mMMtimeToSpawnForCoin;

    int mIcurrentSpawnPointIndex = 0;

    GameObject[] mOspawnPoints;
    GameObject mOspawnedCoin;
    GameObject mOspawnedBee;

    bool mBisBeeSpawnDelayOn = true;
    bool mBisCoinSpawnDelayOn = true;
    // Use this for initialization
    void Start()
    {
        mOspawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }
    // Update is called once per frame
    void Update()
    {
        if (mBisBeeSpawnDelayOn)
        {
            mIcurrentSpawnPointIndex = Random.Range(0, mOspawnPoints.Length - 1);
            CreateBee();
        }

        if (mBisCoinSpawnDelayOn)
        {
            CreateCoinObject();

        }
    }

	//Spawn Bee at random Spawn Point
    void CreateBee()
    {
        mOspawnedBee = (GameObject)Instantiate(mObeePrefab);
        mOspawnedBee.GetComponent<BeeLogic>().mFspeedOfBee = mFspeedOfBee * GetComponent<SpawnerClass>().difficultylevel;
        mOspawnedBee.GetComponent<BeeLogic>().mFattackDelayOfBee = mFattackDelayOfBee;
        mOspawnedBee.GetComponent<BeeLogic>().SpawnPoint = mOspawnPoints[mIcurrentSpawnPointIndex].transform;
        mOspawnedBee.transform.position = mOspawnPoints[mIcurrentSpawnPointIndex].transform.position +
                                        new Vector3(Random.Range(1, 10), Random.Range(1, 10), Random.Range(1, 10));
        mBisBeeSpawnDelayOn = false;
        StartCoroutine("GiveBeeDelay", UnityEngine.Random.Range(mMMtimeToSpawnForBee.Min, mMMtimeToSpawnForBee.Max));
    }
	//Spawn Coin at random Spawn Point
    void CreateCoinObject()
    {
        mOspawnedCoin = (GameObject)Instantiate(mOcoinPrefab);
        int Randomindex = Random.Range(0, mOspawnPoints.Length - 1);
        mOspawnedCoin.transform.position = mOspawnPoints[Randomindex].transform.position;
        mBisCoinSpawnDelayOn = false;
        StartCoroutine("GiveCoinDelay", UnityEngine.Random.Range(mMMtimeToSpawnForCoin.Min, mMMtimeToSpawnForCoin.Max));
    }

    //Coroutine for Spawn Delay
    IEnumerator GiveCoinDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        mBisCoinSpawnDelayOn = true;
    }
    IEnumerator GiveBeeDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        mBisBeeSpawnDelayOn = true;
    }
}
