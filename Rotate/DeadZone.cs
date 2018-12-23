using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadZone : MonoBehaviour
 {
    public GameObject mOfadeOutObject;
    public GameObject mOplayerObject;
    public GameObject mOgameLogic;
    public AudioClip  mACdeadAudioClip;
    AudioSource mASdeadSoundSource;
    // Use this for initialization
    void Start () {
        GetComponent<BoxCollider2D>().isTrigger = true;
        mASdeadSoundSource = gameObject.AddComponent<AudioSource>();
        gameObject.GetComponent<AudioSource>().loop = false;
        gameObject.GetComponent<AudioSource>().clip = mACdeadAudioClip;
    }

    void LoadLevel()
    {
        Application.LoadLevel(Application.loadedLevel);

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            mOgameLogic.GetComponent<PauseManage>().mBisPaused = true;
            mASdeadSoundSource.Play();
            mOplayerObject.transform.localScale = new Vector3(0, 0, 0);
            mOfadeOutObject.GetComponent<FadeOut>().StartFadeAnim();
			//Wait level loading until ending fading
            Invoke("LoadLevel", mOfadeOutObject.GetComponent<FadeOut>().mFanimTime);
        }


    }
   
}
