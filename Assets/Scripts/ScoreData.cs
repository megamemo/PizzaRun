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
        NewBestScore();
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

    private void NewBestScore()
    {
        isBestScore = false;
        if (!Array.Exists(scoreTimes, scoreTimes => scoreTimes == (int)Math.Floor(GameManager.instance.gameDuration)))
        {
            if (bestScoreTime < (int)Math.Floor(GameManager.instance.gameDuration))
            {
                bestScoreTime = (int)Math.Floor(GameManager.instance.gameDuration);
                bestScoreLevel = GameManager.instance.level;
                isBestScore = true;
                BestScore();
            }
            else
            {
                for (int i = 1; i < scoreArrayLenght; i++)
                {
                    if (scoreTimes[i] < (int)Math.Floor(GameManager.instance.gameDuration))
                    {
                        scoreTimes[i] = (int)Math.Floor(GameManager.instance.gameDuration);
                        scoreLevels[i] = GameManager.instance.level;
                        SaveDataFile();
                    }

                }
            }
        }
    }

    private void BestScore()
    {
        scoreTimes[scoreArrayLenght - 1] = bestScoreTime;
        scoreLevels[scoreArrayLenght - 1] = GameManager.instance.level;

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
        scoreTimes = new int[scoreArrayLenght];
        scoreLevels = new int[scoreArrayLenght];

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
