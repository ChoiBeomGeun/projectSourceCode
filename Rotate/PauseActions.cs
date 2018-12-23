/******************************************************************************/
/*!
\file   PauseActions.cs
\author BeomGeun Choi
\brief
This file is for Actions in Pause UI
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class pauseActions : MonoBehaviour 
{
    const int mI_LEVELNUM_Y_OFFSET = 7;

    public GameObject mOpauseUI;
    public GameObject mOmuteObject;
    public GameObject mOunmuteObject;
    public GameObject mOlevelImage;
	
    GameObject mOgameLogic;
    GameObject mOlevelNumber;
    // Use this for initialization
    void Awake () {
        mOgameLogic = GameObject.Find("GAMELOGIC");
        if (AudioListener.volume > 0)
        {
            mOmuteObject.SetActive(true);
            mOunmuteObject.SetActive(false);
        }
        else
        {
            mOmuteObject.SetActive(false);
            mOunmuteObject.SetActive(true);
		}
        InitLevelNumAndImage();
    }
    //function for going back to game 
    public void  BackAction()
    {
        Destroy(mOpauseUI);
        mOgameLogic.GetComponent<PauseManage>().mBisPaused = false;
        mOgameLogic.GetComponent<PauseManage>().PauseAdjust();
    
    }
    //function for restart 
    public void RestartAction()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //function for switching level select scene
    public void LevelSelectAction()
    {
        Destroy(GameObject.Find("BackgroundMusic(Clone)"));
        SceneManager.LoadScene("LevelSelectScene");
    }
    //function for Sound On/off func
    public void VolumeAction()
    {
        if (AudioListener.volume > 0)
        {
            AudioListener.volume = 0;
        }
        else
		{
            AudioListener.volume = 1;
		}
    }
    //Set the level number and level image in Pause UI
    void InitLevelNumAndImage()
    {
        mOlevelNumber = MonoBehaviour.Instantiate(Resources.Load("Prefabs/LevelNum") as GameObject);
        mOlevelNumber.transform.position = mOlevelImage.transform.position;
        mOlevelNumber.transform.SetParent(mOlevelImage.transform);
        mOlevelNumber.transform.position = new Vector3(
            mOlevelNumber.transform.position.x,
            mOlevelNumber.transform.position.y - mI_LEVELNUM_Y_OFFSET,
            mOlevelNumber.transform.position.z);

        mOlevelNumber.GetComponent<UnityEngine.UI.Text>().text = (SceneManager.GetActiveScene().buildIndex).ToString();
        string levelimageDir = "levelimage\\" + (SceneManager.GetActiveScene().buildIndex).ToString();
        mOlevelImage.GetComponent<Image>().sprite
            = Resources.Load(levelimageDir, typeof(Sprite)) as Sprite;

    }
}
