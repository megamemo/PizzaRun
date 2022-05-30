using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public int level { get; private set; }
    private float gameStartTime;
    public float gameDuration { get; private set; }
    private float levelDuration = 30.0f;
    private float TimeScale { get => Time.timeScale; set => Time.timeScale = value; }
    public GameState state;

    [SerializeField] private AudioSource gameMusic;
    [SerializeField] private AudioSource menuMusic;
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
        TimeScale = 1;
        gameStartTime = Time.time;

        startSound.Play();
        menuMusic.Stop();
        gameMusic.Play();
    }

    private void Update()
    {
        ProceedToNextLevel();
        GetGameDuration();
    }

    private float GetGameDuration()
    {
        if (state == GameState.Play)
        {
            gameDuration = Time.time - gameStartTime;
        }
        return gameDuration;
    }

    private void ProceedToNextLevel()
    {
        if (state == GameState.Play)
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

    public void PauseGame()
    {
        state = GameState.Pause;
        TimeScale = 0;
        gameMusic.Pause();
    }

    public void UnPauseGame()
    {
        state = GameState.Play;
        TimeScale = 1;
        gameMusic.UnPause();
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
        state = GameState.StartMenu;

        menuMusic.Play();
    }

    public enum GameState
    {
        StartMenu,
        Play,
        Pause,
        GameOver
    }

}
