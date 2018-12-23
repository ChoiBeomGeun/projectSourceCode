/******************************************************************************/
/*!
\file   FirstPersonEarthLogic.cs
\author BeomGeun Choi
\brief
Earth Logic In first view game.
*/
/******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FirstPersonEarthLogic : MonoBehaviour 
{
    public int mIearthHP = 10;
    public float mFshakePower = 0.01f;
    public float mFtimeToShake = 0.5f;
	
    GameObject mOgameLogic;
	
    bool mBisHitByBee = false;
	
    float mFtimedToSpawn;
	
    Vector3 m_originalPos;
    // Use this for initialization
    void Start () 
	{
        mOgameLogic = GameObject.FindGameObjectWithTag("GameLogic");
        mFtimedToSpawn = mFtimeToShake;
        m_originalPos = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
	{
		//When earth is damaged, it will shake itself
        if (mBisHitByBee)
        {
            if (mFtimedToSpawn > 0)
            {
                mFtimedToSpawn -= Time.deltaTime;
                transform.localPosition = (Vector3)Random.insideUnitCircle * mFshakePower + m_originalPos;
            }
            else
            {
           
                mFtimedToSpawn = mFtimeToShake;
                mBisHitByBee = false;
            }
        }
    }
	//If Bee attacks the earth, earth's size and hp will be decreased
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bee")
        {
            Destroy(other.gameObject);
            mIearthHP--;
            mBisHitByBee = true;
            mFtimedToSpawn = mFtimeToShake;
            Vector3 shrinkedSize =  transform.localScale - new Vector3 (0.5f,0.5f,0.5f);
            transform.localScale = shrinkedSize;
			//If earth's HP is 0, Do lose action and reset the scoreboard
            if (mIearthHP == 0)
            {
                GameObject.FindGameObjectWithTag("ScoreBoard").GetComponent<SumScoreExample>().ResetPoints();
                mOgameLogic.GetComponent<EntireGameLogicInFirstView>().DoLoseAction(); 
            }
        }
    }

}
