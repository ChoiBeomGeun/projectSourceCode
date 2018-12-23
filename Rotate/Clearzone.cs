using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Clearzone : MonoBehaviour
{
    public string mSnextSceneName;
	
    public GameObject mOfadeOutObject;
    public GameObject mOplayer;
    public GameObject mOgameLogic;
	
    public AudioClip mACdeadAudioClip;

    AudioSource mAclearSound ;
    // Use this for initialization
    void Start () 
	{
        Destroy(GetComponent<Rigidbody2D>());
        GetComponent<BoxCollider2D>().isTrigger = true;
        mAclearSound =gameObject.AddComponent<AudioSource>();
        gameObject.GetComponent<AudioSource>().loop = false;
        gameObject.GetComponent<AudioSource>().clip= mACdeadAudioClip;
    }
	
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
		{
			// Check level clear state and set the Progress
            PlayerPrefs.SetInt("level" + (SceneManager.GetActiveScene().buildIndex ), 1);
            if (PlayerPrefs.GetInt("ClearLevel") < SceneManager.GetActiveScene().buildIndex)
            {
				PlayerPrefs.SetInt("ClearLevel", SceneManager.GetActiveScene().buildIndex);
            }
			PlayerPrefs.Save();
			
            mOgameLogic.GetComponent<PauseManage>().mBisPaused = true;
			
			// If NextScene is Level Selection, Destroy BGM of Game 
            if (mSnextSceneName == "LevelSelectScene")
                Destroy(GameObject.Find("BackgroundMusic(Clone)")); 
			
            mAclearSound.Play();
            mOplayer.transform.localScale = new Vector3(0, 0, 0);
            mOfadeOutObject.GetComponent<FadeOut>().StartFadeAnim();
			//Give a delay for the fading out screen
            Invoke("LoadLevel", mOfadeOutObject.GetComponent<FadeOut>().mFanimTime);
        }
    }

    void LoadLevel()
    {
        SceneManager.LoadScene(mSnextSceneName);
    }
}
