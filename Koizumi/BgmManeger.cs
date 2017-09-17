using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManeger : MonoBehaviour {

    private GameObject titleBgm;
    private GameObject gameBgm;
    private GameObject bossBgm;
    private GameObject resultBgm;

    [SerializeField]
    private AudioClip[] audios = new AudioClip[4];

    [HideInInspector]
    public AudioSource audioSource;

    #region Singlton
    public static BgmManeger Instance { get; private set; }
    void Awake()
    {
        if (Instance != null)
        {
            enabled = false;
            DestroyImmediate(this);
            return;
        }
        Instance = this;
    }
    #endregion

	void Start ()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        //----------------------------------------------
        // それぞれのBGMが入ってるオブジェクト取得
        titleBgm = GameObject.Find("TitleBgm");
        gameBgm = GameObject.Find("GameBgm");
        bossBgm = GameObject.Find("BOSSBgm");
        resultBgm = GameObject.Find("ResultBgm");
        //----------------------------------------------

        //-----------------------------
        //取得してきたBGMをすべてアクティブを切ることで疑似的にミュートにする
        titleBgm.SetActive(false);
        gameBgm.SetActive(false);
        bossBgm.SetActive(false);
        resultBgm.SetActive(false);
        //-----------------------------
	}
	
	void Update () 
    {
        //ChangeBGM();
	}

    public void Changed(int num)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = audios[num];
            audioSource.Play();
        }
    }

    private void ChangeBGM()
    {
        //タイトル画面の時
        if(GameController.Instance.gameStates == GameController.GameStates.Title)
        {
            //タイトルのBGMを再生
            titleBgm.SetActive(true);  
        }
        //リザルト画面に入ったら
        else if (GameController.Instance.gameStates == GameController.GameStates.Result)
        {
            //ボス前でやられる可能性もあるので道中のBGMを切る
            gameBgm.SetActive(false);
            //ボスのBGM切る
            bossBgm.SetActive(false);
            //リザルトのBGMを再生する
            resultBgm.SetActive(true);
        }
        else if (GameController.Instance.gamePhase == GameController.GamePhase.Phase5)
        {
            //道中のBGMを切る
            gameBgm.SetActive(false);
            //ボスのBGMを再生
            bossBgm.SetActive(true);
            return;
        }
        //ゲームが開始したら
        else if(GameController.Instance.gameStates == GameController.GameStates.Start)
        {
            //タイトルのBGMを切る
            titleBgm.SetActive(false);
            //道中のBGMを再生
            gameBgm.SetActive(true);
        }
      

    }
}
