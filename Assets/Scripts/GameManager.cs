using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public int level { get; private set; }
    private float gameStartTime;
    public float gameDuration { get; private set; }
    private float levelDuration = 30.0f; //Designed duration is 30.0f
    private float TimeScale { get => Time.timeScale; set => Time.timeScale = value; }
    public GameState gameState;
    public TimeState timeState;

    [SerializeField] private AudioSource gameMusic;
    [SerializeField] private AudioSource menuMusic;
    [SerializeField] private AudioSource startSound;
    [SerializeField] private AudioSource gameOverSound;

    public event EventHandler GameRestarted;
    public event EventHandler LevelChanged;
    public event EventHandler StartMenuStarted;
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
        gameState = GameState.Play;
        SetLevel(1);
        TimeScale = 1;
        WorkSlowmotion(TimeState.Normal);
        gameStartTime = Time.time;

        startSound.Play();
        menuMusic.Stop();
        gameMusic.Play();
        gameMusic.volume = 1;
    }

    private void Update()
    {
        ProceedToNextLevel();
        GetGameDuration();
    }
    
    private float GetGameDuration()
    {
        if (gameState == GameState.Play)
        {
            gameDuration = Time.time - gameStartTime;
        }
        return gameDuration;
    }

    private void ProceedToNextLevel()
    {
        if (gameState == GameState.Play)
        {
            if (gameDuration >= levelDuration * level)
            SetLevel(level + 1);    
        }
    }

    private void SetLevel(int value)
    {
        level = value;
        LevelChanged?.Invoke(this, EventArgs.Empty);
    }

    public void GameOver()
    {
        gameState = GameState.GameOver;
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

    public void PauseGame()
    {
        gameState = GameState.Pause;
        gameMusic.Pause();

        TimeScale = 0;
    }

    public void UnPauseGame()
    {
        gameState = GameState.Play;
        gameMusic.UnPause();

        if (timeState == TimeState.Normal)
            TimeScale = 1;

        if (timeState == TimeState.SlowMotion)
            TimeScale = 0.5f;
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

    public void SetStartMenuState()
    {
        gameState = GameState.StartMenu;

        StartMenuStarted?.Invoke(this, EventArgs.Empty);

        menuMusic.Play();
    }

    public void WorkSlowmotion(TimeState state)
    {
        timeState = state;

        if (timeState == TimeState.Normal)
            StopSlowmotion();

        if (timeState == TimeState.SlowMotion)
            StartSlowmotion();
    }

    private void StartSlowmotion()
    {
        TimeScale = 0.5f;
        gameMusic.volume = 0.33f;
    }

    private void StopSlowmotion()
    {
        TimeScale = 1.0f;
        gameMusic.volume = 1.0f;
    }

    public enum GameState
    {
        StartMenu,
        Play,
        Pause,
        GameOver
    }

    public enum TimeState
    {
        Normal,
        SlowMotion
    }

}
