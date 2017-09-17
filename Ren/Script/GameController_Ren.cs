using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController_Ren : MonoBehaviour {

    private int nameCount = 0;

    public static GameController_Ren Instance;

	// Use this for initialization
	void Start () {
        //ファイルを読み込み
        ScoreInfo.Score.Name = "Name "+nameCount;
        ScoreInfo.Read();
        foreach(Score s in ScoreInfo.Scores)
        {
            Debug.Log(s.Name + " <=> Total :"+ScoreInfo.GetTotalScore(s));
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ScoreInfo.HitUfo();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            ScoreInfo.HitMeteorSmall();
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            ScoreInfo.HitMeteorNormal();
        }
        else if(Input.GetKeyDown(KeyCode.F))
        {
            ScoreInfo.HitMeteorBig();
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            ScoreInfo.HitMars();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            //セーブ
            //Debug.ClearDeveloperConsole();
            
            //ハイスコアを確認して、今の点数が今までのハイスコア5つと比べて何番目かを確認している
            ScoreInfo.AddToHighScores();
            //Debug.Log(ScoreInfo.GetTotalScore());
            nameCount++;

            //スコアを新しく作る
            ScoreInfo.Score = new Score();

            //気にしない
            ScoreInfo.Score.Name = "Name " + nameCount;
        }
        //else if (Input.GetKeyDown(KeyCode.W))
        //{

        //    ScoreInfo.SortScoreDescending();
        //}
	}
}
