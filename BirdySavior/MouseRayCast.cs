/******************************************************************************/
/*!
\file   MouseRayCast.cs
\author BeomGeun Choi
\brief
In the first view game, mouse ray casting for the first view game
*/
/******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRayCast : MonoBehaviour 
{
    public Transform target;
    public Ray mRray;
    public RaycastHit mRHhitInfo;
	
    GameObject mOgameLogic;

    private void Awake()
    {
        mOgameLogic = GameObject.FindGameObjectWithTag("GameLogic");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) // left mouse button is clicked and game state is not paused or lose
			&& !mOgameLogic.GetComponent<EntireGameLogicInFirstView>().mBisPaused
            && !mOgameLogic.GetComponent<EntireGameLogicInFirstView>().mBisLose)  
        {
			//Check Input Position And Find Raycast Target
            mRray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(mRray, out mRHhitInfo))
                {
					//Player touches Bee
                    if (mRHhitInfo.transform.gameObject.tag == "Bee")
                    {
                        GameObject.FindGameObjectWithTag("ScoreBoard").GetComponent<SumScoreExample>().AddPoints(100);

                        GameObject.FindGameObjectWithTag("Gun").GetComponent<GunRotation>().currentDeadBee
                            = mRHhitInfo.transform.gameObject;
                        mRHhitInfo.transform.gameObject.GetComponent<Animator>().enabled = false;
                        mRHhitInfo.transform.gameObject.GetComponent<Animation>().Play("BeeDead");
                        StartCoroutine(BeeDeadCoroutine(mRHhitInfo.transform.gameObject, 0.15f));
                      
                    }
						//Player touches Coin
                    if (mRHhitInfo.transform.gameObject.tag == "Coin")
                    {
                        GameObject.FindGameObjectWithTag("ScoreBoard").GetComponent<SumScoreExample>().AddCoins(1);
                        mRHhitInfo.transform.gameObject.GetComponent<AudioSource>().Play();
                        Destroy(mRHhitInfo.transform.gameObject,0.1f);
                    }
                }
            }
        }
    }
	//Coroutine for Playing Bee animation
	IEnumerator BeeDeadCoroutine(GameObject beeobj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(beeobj);
    }

}
