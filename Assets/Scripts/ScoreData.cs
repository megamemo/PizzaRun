using UnityEngine;
using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;

public class ScoreData : MonoBehaviour
{
    public static ScoreData instance { get; private set; }
    public int bestScoreTime { get; private set; }
    public int bestScoreLevel { get; private set; }
    public bool isNewBestScore { get; private set; }

    public int scoreArrayLength { get; private set; } = 3;
    public int[] scoreTimes { get; private set; }
    public int[] scoreLevels { get; private set; }


    private void Awake()
    {
        InstanciateScoreData();
    }

    private void Start()
    {
        GameManager.instance.GameOvered += OnGameOvered;
    }

    private void OnDestroy()
    {
        if (GameManager.instance != null)
            GameManager.instance.LevelChanged -= OnGameOvered;
    }

    private void OnGameOvered(object sender, EventArgs e)
    {
        CheckBestScore();
    }

    private void InstanciateScoreData()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    
        LoadDataFile();
    }

    private void CheckBestScore()
    {
        isNewBestScore = false;

        int gameDuration = (int)Math.Floor(GameManager.instance.gameDuration);
        int gameLevel = GameManager.instance.level;

        if (Array.Exists(scoreTimes, scoreTimes => scoreTimes == gameDuration))
            return;
       
        if (bestScoreTime < gameDuration)
        {
            bestScoreTime = gameDuration;
            bestScoreLevel = gameLevel;
            isNewBestScore = true;

            RecordBestScore(gameLevel);
        }
        else
        {
            for (int i = 1; i < scoreTimes.Length; i++)
            {
                if (scoreTimes[i] < gameDuration)
                    continue;

                scoreTimes[i] = gameDuration;
                scoreLevels[i] = gameLevel;

                SaveDataFile();
            }
        }
    }

    private void RecordBestScore(int level)
    {
        scoreTimes[scoreTimes.Length - 1] = bestScoreTime;
        scoreLevels[scoreLevels.Length - 1] = level;

        Array.Sort(array: scoreTimes, comparer: new InverseCompare());
        Array.Sort(array: scoreLevels, comparer: new InverseCompare());

        SaveDataFile();
    }

    private sealed class InverseCompare : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return y.CompareTo(x);
        }
    }

    [Serializable]
    class SaveData
    {
        public int[] scoreTimes;
        public int[] scoreLevels;
    }

    private void SaveDataFile()
    {
        SaveData data = new SaveData();
       
        data.scoreTimes = scoreTimes;
        data.scoreLevels = scoreLevels;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    private void LoadDataFile()
    {
        scoreTimes = new int[scoreArrayLength];
        scoreLevels = new int[scoreArrayLength];

        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            scoreTimes = data.scoreTimes;
            scoreLevels = data.scoreLevels;

            bestScoreTime = scoreTimes.Max();
            bestScoreLevel = scoreLevels.Max();
        }
    }

}
