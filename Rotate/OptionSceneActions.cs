/******************************************************************************/
/*!
\file   OptionSceneActions.cs
\author BeomGeun Choi
\brief
This file is for Actions in Option Scene
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class OptionSceneAction : MonoBehaviour 
{
    public GameObject mOmuteObject;
    public GameObject mOunmuteObject;
	
    bool mBisCreditOn = false;
	
    GameObject mOcreditTextObject;
    // Use this for initialization
    void Start () 
	{
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
        mOcreditTextObject = GameObject.Find("credittext");
        mOcreditTextObject.SetActive(false);
    }
	//Game Sound On/Off function
    public void VolumeSettingAction()
    {
        if (AudioListener.volume > 0)
        {
            AudioListener.volume = 0;
        }
        else
            AudioListener.volume = 1;
    }
	//Go Back to main menu
    public void ReturnAction()
    {
        SceneManager.LoadScene("Main");
    }
	//Enable or disable the credit text
    public void CreditAction()
    {
        mBisCreditOn = !mBisCreditOn;
        mOcreditTextObject.SetActive(mBisCreditOn);
    }
	//Switch language setting between English and Korea
    public void LanauageAction()
    {
        if (PlayerPrefs.GetString("LANG") == "EN")
        {
            PlayerPrefs.SetString("LANG", "KR");
        }
        else
        {
            PlayerPrefs.SetString("LANG", "EN");
        }
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
