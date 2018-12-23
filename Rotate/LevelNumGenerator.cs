/******************************************************************************/
/*!
\file   LevelNumGenerator.cs
\author BeomGeun Choi
\brief
This file is for generating level number in Level Selection Scene
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelNumGenerator : MonoBehaviour 
{
	//todo
	//fix magic number
    const int mIMAXLEVEL = 26;
	
    public int[] LevelClearList = new int[26];
    public int NumberOfLevels = 26;

    void Start() 
	{

        for (int i = 0; i < NumberOfLevels; i++)
        {
          //  PlayerPrefs.SetInt("level" + i.ToString(), 0);
            LevelClearList[i] = PlayerPrefs.GetInt("level" + i.ToString());


        }
		//Always Set First level is cleared
        LevelClearList[0] = 1;
		//Spawn the level number image
        for (int i =1; i <= mIMAXLEVEL; i++)
        {
            
            GameObject NumObject = MonoBehaviour.Instantiate(Resources.Load("Prefabs/LevelNum") as GameObject);
            

            NumObject.transform.SetParent(GameObject.Find(i.ToString()).transform);
            NumObject.GetComponent<UnityEngine.UI.Text>().text = i.ToString();
            NumObject.transform.position = GameObject.Find(i.ToString()).transform.position;
            NumObject.transform.position = new Vector3(
                NumObject.transform.position.x, 
                NumObject.transform.position.y-15,
                NumObject.transform.position.z);
           
        }
		//Spawn the lock image
        for (int indexOfLockimage = 2; indexOfLockimage < mIMAXLEVEL; indexOfLockimage++)
        {
            if (LevelClearList[indexOfLockimage-1] == 0)
            {
                GameObject NumObject = MonoBehaviour.Instantiate(Resources.Load("Prefabs/LockImage") as GameObject);
                NumObject.transform.SetParent(GameObject.Find(indexOfLockimage.ToString()).transform);
                NumObject.transform.position = GameObject.Find(indexOfLockimage.ToString()).transform.position;
                NumObject.transform.position = new Vector3(
                    NumObject.transform.position.x,
                    NumObject.transform.position.y - 15,
                    NumObject.transform.position.z);
            }
        }
        if (LevelClearList[25] == 0)
        {
            GameObject NumObject = MonoBehaviour.Instantiate(Resources.Load("Prefabs/LockImage") as GameObject);
            NumObject.transform.SetParent(GameObject.Find("26").transform);
            NumObject.transform.position = GameObject.Find("26").transform.position;
            NumObject.transform.position = new Vector3(
                NumObject.transform.position.x,
                NumObject.transform.position.y - 15,
                NumObject.transform.position.z);
        }

    }
}
