/******************************************************************************/
/*!
\file   FadeOut.cs
\author BeomGeun Choi
\brief
This file is for the "fade out" effect
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadeOut : MonoBehaviour 
{
    public float mFanimTime = 2f;
	
	float mFstart = 0f;
    float mFend = 1f;
    float mFtime = 0f;
	
    bool bisPlaying = false;

	Image mIMGfadeImage;
	
    void Awake ()
	{
        mIMGfadeImage = GetComponent<Image>();
    }
	
	public void StartFadeAnim()
    {
        if (bisPlaying)
            return;

        StartCoroutine("PlayFadeOut");
    }

	IEnumerator PlayFadeOut()
    {
        bisPlaying = true;
        Color color = mIMGfadeImage.color;
        mFtime = 0f;
        color.a = Mathf.Lerp(mFstart, mFend, mFtime);
        while (color.a < 1f)
        {
            mFtime += Time.deltaTime / mFanimTime;
            color.a = Mathf.Lerp(mFstart, mFend, mFtime);
            mIMGfadeImage.color = color;
            yield return null;
        }
        bisPlaying = false;

    }
}
