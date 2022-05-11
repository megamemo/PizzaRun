using UnityEngine;
using System.IO;
using System;

public class ScoreData : MonoBehaviour
{
    public static ScoreData instance;
    public int bestScoreTime;
    public int bestScoreLevel;
    public bool isBestScore;

    public int scoreArrayLenght = 3;
    public int[] scoreTimes;
    public int[] scoreLevels;


    private void Awake()
    {
        InstanceScoreData();
        scoreTimes = new int[scoreArrayLenght];
        scoreLevels = new int[scoreArrayLenght];
        LoadDataFile();
    }

    private void InstanceScoreData()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void NewBestScore()
    {
        isBestScore = false;

        if (bestScoreTime < (int)Math.Floor(GameManager.instance.playTime))
        {
            bestScoreTime = (int)Math.Floor(GameManager.instance.playTime);
            bestScoreLevel = GameManager.instance.levelCurrent;
            isBestScore = true;
            BestScores();
        }
    }

    public void BestScores()
    {
        scoreTimes[scoreArrayLenght - 1] = bestScoreTime;
        scoreLevels[scoreArrayLenght - 1] = GameManager.instance.levelCurrent;

        Array.Sort(scoreTimes);
        Array.Sort(scoreLevels);
        Array.Reverse(scoreTimes);
        Array.Reverse(scoreLevels);

        SaveDataFile();
    }

    [Serializable]
    class SaveData
    {
        public int bestScoreTime;
        public int bestScoreLevel;
        public int[] scoreTimes;
        public int[] scoreLevels;
    }

    private void SaveDataFile()
    {
        SaveData data = new SaveData();
        data.bestScoreTime = bestScoreTime;
        data.bestScoreLevel = bestScoreLevel;
        data.scoreTimes = scoreTimes;
        data.scoreLevels = scoreLevels;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    private void LoadDataFile()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScoreTime = data.bestScoreTime;
            bestScoreLevel = data.bestScoreLevel;
            scoreTimes = data.scoreTimes;
            scoreLevels = data.scoreLevels;
        }
    }

}
