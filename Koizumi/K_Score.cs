using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class K_Score : MonoBehaviour
{
    [HideInInspector]
    public int meteoDefeatCount;
    [HideInInspector]
    public int ufoDefeatCount;
    [HideInInspector]
    public int marsDefeatCount;
    public int meteoScore = 5;
    public int ufoScore = 10;
    public int marsScore = 50;
    private int totalScore;
    private int hightScore;
    private int scoreCount = 0;

    public Text hightScoreTex;
    [HideInInspector]
    public bool hitMeteo = false;
    [HideInInspector]
    public bool hitUfo = false;
    [HideInInspector]
    public bool hitMars = false;

    private string key = "HIGH SCORE";

    #region Singlton
    public static K_Score Instance { get; private set; }
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

    void Start()
    {
        hightScore = PlayerPrefs.GetInt(key, 0);
        hightScoreTex.text = "HighScore:" + hightScore.ToString();
    }

    void Update()
    {

        hightScoreTex.text = "Score:" + ScoreInfo.GetTotalScore();

        /*
        if(GameController.Instance.gameStates == GameController.GameStates.End)
        {
            if (scoreCount < 1)
            {
                scoreCount++;

                totalScore = (meteoDefeatCount * meteoScore) + (ufoDefeatCount * ufoScore) + (marsDefeatCount * marsScore);

                if(totalScore > hightScore)
                {
                    hightScore = totalScore;
                    PlayerPrefs.SetInt(key, hightScore);
                    hightScoreTex.text = "Score:" + ScoreInfo.GetTotalScore();
                }
            }
        }*/

        //  スペース押すとハイスコアの初期化
        if(Input.GetKeyDown("space"))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
