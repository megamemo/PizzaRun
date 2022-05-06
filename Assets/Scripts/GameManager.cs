using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int levelCurrent = 1;
    public float playTime;
    public float levelTime = 30.0f;
    public bool gameStopped;

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private Button pauseButton;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI yourScoreText;

    public AudioSource gameMusic;
    [SerializeField] private AudioSource gameOverSound;


    void Awake()
    {
        instance = this;
        Time.timeScale = 1;
        gameOverText.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);

        levelText.text = "LEVEL " + (levelCurrent);

        InvokeRepeating("NextLevel", levelTime, levelTime);
    }

    private void FixedUpdate()
    {
        if (!gameStopped)
        {
            playTime += Time.fixedDeltaTime;
        }
    }

    private void NextLevel()
    {
        if (!gameStopped)
        {
            levelCurrent++;
            levelText.text = "LEVEL " + (levelCurrent);
        }
    }

    public void GameOver()
    {
        gameMusic.Stop();
        gameOverSound.Play();
        gameStopped = true;
        gameOverText.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        ScoreData.instance.NewBestScore();
        GameOverScore();
    }

    private void GameOverScore()
    {
        if (ScoreData.instance.isBestScore)
        {
            bestScoreText.text = "NEW RECORD !";
        }
        else
        {
            bestScoreText.text = "Best Score: " + ScoreData.instance.bestScoreTime + " sec, " + ScoreData.instance.bestScoreLevel + " lvl";
        }

        yourScoreText.text = "Your Score: " + (int)Math.Floor(playTime) + " sec, " + levelCurrent + " lvl";
    }
}
