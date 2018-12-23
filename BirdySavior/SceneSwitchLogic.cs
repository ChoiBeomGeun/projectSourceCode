/******************************************************************************/
/*!
\file   SceneSwitchLogic.cs
\author BeomGeun Choi
\brief
Abstract class for SceneSwitch Logic
*/
/******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitchLogic : MonoBehaviour {

    public float mFneededScoreToMove = 2000;

    public bool mBisLose = false;

    public bool mBisPaused = false;

    protected GameObject mOscoreBoard;

    public GameObject mOplayer;

    protected bool mBisSceneChanging = false;

    protected int mIfirstScore = 0;

    public virtual void DoLoseAction(){ }

    public virtual void DoPauseAction() { }

}
