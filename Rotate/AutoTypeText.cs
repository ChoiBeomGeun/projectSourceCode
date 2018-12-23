/******************************************************************************/
/*!
\file   AutoTypeText.cs
\author BeomGeun Choi
\brief
AutoTypeText For MessageBox
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LitJson;
public class AutoTypeText : MonoBehaviour 
{
    public GameObject mOgameLogic ;
    public GameObject mOmessageBox;
    public GameObject mOnextMessage;
    public Text mTtextInterface;//Visible UI Text for Script.
    public string mSjsonToRead;
	
    List<string> mLSscripts = new List<string>();
    List<string> mLSchainText = new List<string>(); //
	
	float mFvelocityMessage {get; set;}  //velocity of between letters
	
    bool mBisPrinted = false;
    bool mBisSkipped = false;
    bool mBisFistMessage = true;
	
    int mIcurrentLine = 0;
    int mIsizeOfScripts;
	
    void Start()
    {
        mFvelocityMessage = 0.1f;
       
		
		//Change Instruction Message as the lanauage setting
        if (PlayerPrefs.GetString("LANG") == "KR")
        {
            mOnextMessage.GetComponent<UnityEngine.UI.Text>().text
            = "다음 메시지 보기(터치)";
        }
        else
        {
            mOnextMessage.GetComponent<UnityEngine.UI.Text>().text
            = "Next Message (Touch)";
        }

		//Read the Json File from "Resources//Script" folder
        TextAsset mTAscripts = (TextAsset)Resources.Load("Scripts\\" + mSjsonToRead, typeof(TextAsset));
        JsonData TextAssetToJson = JsonMapper.ToObject(mTAscripts.ToString());


        mIsizeOfScripts = int.Parse(TextAssetToJson["NumberOfScripts"].ToString());

        for (int i = 0; i < mIsizeOfScripts; i++)
            mLSscripts.Add(TextAssetToJson[PlayerPrefs.GetString("LANG")][i].ToString());
		
        mOgameLogic.GetComponent<PauseManage>().mBisPaused = true;
        mBisPrinted = true;
        mLSchainText = mLSscripts;

    }

    private void Update()
    {
    
		//Condition For Checking the end of script and destory messagebox
        if (mOmessageBox //Check if Message is not NULL
		&&((Input.GetKeyDown("space") 
		||SimpleInput.GetButtonDown("space")) // Handle Input
		&&mLSscripts.Capacity!=0 // Check if Script is not NULL  
		&&(mLSscripts.Count == mIcurrentLine ))) // Check the end of Script
        {
            mOgameLogic.GetComponent<PauseManage>().mBisPaused = false ;
            Destroy(mOmessageBox);

        }

		//Condition For Typying the Next Script 
        if (mBisFistMessage 
		||( (Input.GetKeyDown("space")|| SimpleInput.GetButtonDown("space"))
		&&mBisPrinted // Check if messagebox is printed
		&&(mLSchainText.Capacity-1 >= mIcurrentLine))) // Check if the Current line doesnt pass the Scripts
        StartCoroutine(WritemLSchainText(mLSchainText[mIcurrentLine]));

		//Condition For Skipping current line
        if (!mBisPrinted 
		&& (Input.GetKeyDown("space") 
		|| SimpleInput.GetButtonDown("space")))
            SkipMessage();
    }

    IEnumerator WritemLSchainText(string mLSchainText)
    {
		//Coroutine For MessageBox Typying
        while (mLSchainText.Length > 0 )
        {
            mBisPrinted = false;
            mBisFistMessage = false;
            if (mBisSkipped)
            {
                mBisSkipped = false;
                mTtextInterface.text = mLSscripts[mIcurrentLine];
                break;
            }
            mTtextInterface.text += mLSchainText.Substring(0, 1); //Write first letter in mLSchainText.
            mLSchainText = mLSchainText.Substring(1, mLSchainText.Length - 1); //We take the rest in mLSchainText, less the first letter.
            yield return new WaitForSeconds(mFvelocityMessage);
                
        }
		// Print Next Script
        mIcurrentLine++;
        mBisPrinted = true; 
    }

    public void SkipMessage()
    {
        mBisSkipped = true;
    }
       
}
