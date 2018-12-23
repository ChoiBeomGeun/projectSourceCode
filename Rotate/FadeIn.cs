/******************************************************************************/
/*!
\file   FadeIn.cs
\author BeomGeun Choi
\brief
This file is for the "fade in" effect
*/
/******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeIn : MonoBehaviour 
{
    public float mFanimTime = 2f;
    private Image mIMGfadeImage;

    private float mFstart = 1f;
    private float mFend = 0f;
    private float mFtime = 0f;
    // Use this for initialization

    void Awake()
    {
        mIMGfadeImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayFadeIn();
    }
    void PlayFadeIn()
    {
        mFtime += Time.deltaTime / mFanimTime;

        Color color = mIMGfadeImage.color;
        color.a = Mathf.Lerp(mFstart, mFend, mFtime);

        mIMGfadeImage.color = color;


    }
}
