using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameResultManage : MonoBehaviour
 {
    public Text mTscoreField; // Text field displaying current score
    public Text mThighscoreField; // Text field displaying high score
    public Text mTcoinField; // Text field displaying coin score

    public GameObject mOsoundIcon;
    public GameObject mOsoundText;
    public Sprite mSmuteImage;
    public Sprite mSunmuteImage;
	
    AudioSource mASclickSound;
	
    GameObject mOscoreBoard;
    GameObject mOgameLogic;
	
    float mFcurrentVolume= 1;
    
    void Awake ()
    {
       //Init Mute Image as the volume
        mFcurrentVolume = AudioListener.volume;
		if(AudioListener.volume < 0)
        {
            mOsoundIcon.GetComponent<UnityEngine.UI.Image>().sprite = mSmuteImage;
            mOsoundText.GetComponent<Text>().text = "UnMute";
        }
        else
        {
            mOsoundIcon.GetComponent<UnityEngine.UI.Image>().sprite = mSunmuteImage;
            mOsoundText.GetComponent<Text>().text = "Mute";
        }
		//Init the result Message as the scoreboard
        mOscoreBoard = GameObject.FindGameObjectWithTag("ScoreBoard");
        mOgameLogic = GameObject.FindGameObjectWithTag("GameLogic");

        mASclickSound = GameObject.Find("ClickSound").GetComponent<AudioSource>();
        mASclickSound.Play();
		//Getting ScoreInformation from ScoreBoard
        mTscoreField.text = mOscoreBoard.GetComponent<SumScoreExample>().CheckCurrentScore().ToString();
        mThighscoreField.text = mOscoreBoard.GetComponent<SumScoreExample>().CheckHighScoreAndReuturn().ToString();
        mTcoinField.text = mOscoreBoard.GetComponent<SumScoreExample>().CheckCcins().ToString();

        mOscoreBoard.SetActive(false);
    }
	//function for restart 
	public void RestartAction()
    {
        mASclickSound.Play();
        Destroy(mOscoreBoard);
		DelaySceneLoad("EarthRunner",2f);
    }
	//function for going back to game 
    public void ResumeAction()
    {
        mASclickSound.Play();
        mOgameLogic.GetComponent<SceneSwitchLogic>().DoPauseAction();

    }
	//function for Sound On/off func
    public void MuteAction()
    {
       if(AudioListener.volume > 0)
        {
            AudioListener.volume = 0;
            mOsoundIcon.GetComponent<UnityEngine.UI.Image>().sprite = mSmuteImage;
            mOsoundText.GetComponent<Text>().text = "UnMute";
        }
        else
        {
            AudioListener.volume = mFcurrentVolume;
            mOsoundIcon.GetComponent<UnityEngine.UI.Image>().sprite = mSunmuteImage;
            mOsoundText.GetComponent<Text>().text = "Mute";
        }
        mASclickSound.Play();
    }
	//function for going back to main menu 
    public void MenuAction()
    {
        Destroy(mOscoreBoard);
        mASclickSound.Play();
    	DelaySceneLoad("MainScene",2f);

    }
	//Delay Level Loading Function for play sound and fading image
    IEnumerator DelaySceneLoad(string scenename, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(scenename);

    }
}

