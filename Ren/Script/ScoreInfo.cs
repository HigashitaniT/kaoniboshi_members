using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;

public static class ScoreInfo {
    
    public static Score Score = new Score();

    public static List<Score> Scores = new List<Score>();
    
    public static void HitUfo()
    {
        ScoreInfo.Score.Ufo++;
    }
    public static void HitMeteorSmall()
    {
        ScoreInfo.Score.MeteorSmall++;
    }
    public static void HitMeteorNormal()
    {
        ScoreInfo.Score.MeteorNormal++;
    }
    public static void HitMeteorBig()
    {
        ScoreInfo.Score.MeteorBig++;
    }
    public static void HitMars()
    {
        ScoreInfo.Score.Mars++;
        ScoreInfo.Score.isWin = true;
    }
    public static int GetTotalScore()
    {
        int total = 0;
        if (GameController.Instance.IsWin)
        {
            total = ((Score.Ufo * 10) + (Score.MeteorSmall * 5) + (Score.MeteorNormal * 10) + (Score.MeteorBig * 20) + (Score.Mars * Score.hp * 50));
        }else{
            total = ((Score.Ufo * 10) + (Score.MeteorSmall * 5) + (Score.MeteorNormal * 10) + (Score.MeteorBig * 20) + (Score.Mars * 50));
        }
        return total;
    }
    public static int GetTotalScore(Score s)
    {
        int total = 0;
        if (s.isWin)
        {
            total = ((s.Ufo * 10) + (s.MeteorSmall * 5) + (s.MeteorNormal * 10) + (s.MeteorBig * 20) + (s.Mars * 50 * s.hp));
        }
        else
        {
            total = ((s.Ufo * 10) + (s.MeteorSmall * 5) + (s.MeteorNormal * 10) + (s.MeteorBig * 20) + (s.Mars * 50));
        }
        return total;
    }
    

    //ファイルを書き込み
    public static void Save()
    {
        Hashtable hash = new Hashtable();

        // https://msdn.microsoft.com/en-us/library/system.runtime.serialization.formatters.binary.binaryformatter(v=vs.110).aspx
        SortScoreDescending();
        hash.Add("scores",Scores);

        DESCryptoServiceProvider des = new DESCryptoServiceProvider();

        FileStream fs = new FileStream(Application.dataPath + "/DataFile.dat", FileMode.Create,FileAccess.Write);

        byte[] key = { 1, 2, 3, 4, 5, 6, 7, 8 };
        byte[] iv = { 1, 2, 3, 4, 5, 6, 7, 8 };

        BinaryFormatter formatter = new BinaryFormatter();

        CryptoStream cs = new CryptoStream(fs, des.CreateEncryptor(key, iv), CryptoStreamMode.Write);
        try
        {
            formatter.Serialize(cs,hash);
            Debug.Log("Saved");
            cs.Close();
        }
        catch (SerializationException e)
        {
            Debug.Log("Failed to serialize. Reason: " + e.Message);
            throw;
        }
        finally
        {
            
            fs.Close();

        }
    }
    //ファイルを読み込み
    public static void Read()
    {
        InitData();
        Hashtable hash = null;

        DESCryptoServiceProvider des = new DESCryptoServiceProvider();

        byte[] key = { 1, 2, 3, 4, 5, 6, 7, 8 };
        byte[] iv = { 1, 2, 3, 4, 5, 6, 7, 8 };

        try
        {
            FileStream fs = new FileStream(Application.dataPath + "/DataFile.dat", FileMode.Open);

            CryptoStream cs = new CryptoStream(fs, des.CreateDecryptor(key, iv), CryptoStreamMode.Read);

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                hash = (Hashtable)formatter.Deserialize(cs);
                cs.Close();
            }
            catch (SerializationException e)
            {
                Debug.Log("Failed to deserialize. Reason: " + e.Message);
                //throw;
            }
            finally
            {
                fs.Close();
                
            }
        }
        catch (FileNotFoundException ex)
        {
            Debug.Log("Failed to Load. Reason: " + ex.Message);
            
            //throw;
        }
        if(hash!=null)
            Scores = (List<Score>)hash["scores"];
        
        /*foreach (DictionaryEntry de in hash)
        {
            Debug.Log("{0} lives at {1}. " + de.Key +" - "+ de.Value);
        }*/
        
    }
    public static void InitData()
    {
        ScoreInfo.Score.Name = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        
    }
    public static void ResetHighScore()
    {
        Scores = new List<Score>();
        Save();
    }

    public static void AddToHighScores()
    {
        
        Debug.Log(Scores.Count);
        SortScoreDescending();
        if (Scores.Count < 5)
        {
            
            Scores.Add(Score);
            SortScoreDescending();
            Save();
        }
        else if(Scores.Count == 5)
        {
            if (GetTotalScore() > GetTotalScore(Scores[Scores.Count - 1]))
            {
                Scores.Add(Score);
                SortScoreDescending();
                Scores.RemoveAt(Scores.Count-1);
                
                Save();
            }
        }
    }
    public static void SortScoreAscending()
    {

        for (int i = 0; i < Scores.Count; i++)
        {
            for (int j = i; j > 0; j--)
            {
                if (GetTotalScore(Scores[j - 1]) > GetTotalScore(Scores[j]))
                {
                    Score temp = Scores[j - 1];
                    Scores[j - 1] = Scores[j];
                    Scores[j] = temp;
                }
            }
        }
    }
    public static void SortScoreDescending(){

        for (int i = 0; i < Scores.Count; i++)
        {
            for (int j = i; j > 0; j--)
            {
                if (GetTotalScore(Scores[j - 1]) < GetTotalScore(Scores[j])) //スコアが同数だった場合はどうするのか？
                {
                    Score temp = Scores[j - 1];
                    Scores[j - 1] = Scores[j];
                    Scores[j] = temp;
                }
            }
        }
        foreach (Score s in ScoreInfo.Scores)
        {
            //Debug.Log(s.Name + " <=> Total :" + ScoreInfo.GetTotalScore(s));
        }
    }
}
