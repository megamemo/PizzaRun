using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public int level { get; private set; }
    private float gameStartTime;
    public float gameDuration { get; private set; }
    private float levelDuration = 30.0f;
    public float timeScale { get => Time.timeScale; set => Time.timeScale = value; }
    public GameState state;

    public AudioSource gameMusic;
    [SerializeField] private AudioSource startSound;
    [SerializeField] private AudioSource gameOverSound;

    public event EventHandler GameRestarted;
    public event EventHandler LevelChanged;
    public event EventHandler GameOvered;


    private void Awake()
    {
        InstanciateGameManager();
    }

    private void InstanciateGameManager()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void StartGame()
    {
        state = GameState.Play;
        SetLevel(1);
        timeScale = 1;
        gameStartTime = Time.time;

        startSound.Play();
        gameMusic.Play();
    }

    private void Update()
    {
        if (state == GameState.Play)
        {
            if (GetGameDuration() >= levelDuration * level)
                ProceedToNextLevel();
        }
    }

    private float GetGameDuration()
    {
        return gameDuration = Time.time - gameStartTime;
    }

    private void ProceedToNextLevel()
    {
        SetLevel(level + 1);
    }

    private void SetLevel(int value)
    {
        level = value;
        LevelChanged?.Invoke(this, EventArgs.Empty);
    }

    public void GameOver()
    {
        state = GameState.GameOver;
        GameOvered?.Invoke(this, EventArgs.Empty);
        gameMusic.Stop();
        gameOverSound.Play();
        GetGameDuration();
    }

    public void RestartGame()
    {
        gameMusic.Stop();
        gameOverSound.Stop();
        gameDuration = 0;

        ResetGame();
        StartGame();
    }

    public void StopGame()
    {
        gameMusic.Stop();

        ResetGame();
    }

    private void ResetGame()
    {
        GameRestarted?.Invoke(this, EventArgs.Empty);
    }

    public enum GameState
    {
        StartMenu,
        Play,
        Pause,
        GameOver
    }

}
