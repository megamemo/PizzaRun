using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private AudioSource menuSound;
    [SerializeField] private AudioSource exitSound;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI yourScoreText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Button pauseButton;


    private void Awake()
    {
        bestScoreText.text = "Best Score: " + ScoreData.instance.bestScoreTime + " sec, " + ScoreData.instance.bestScoreLevel + " lvl";
    }

    private void Start()
    {
        StartGameUI();

        GameManager.instance.LevelChanged += OnLevelChanged;
        GameManager.instance.GameOvered += OnGameOvered;
    }

    private void StartGameUI()
    {
        UpdateLevelText();

        pauseMenu.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        gameOverMenu.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseMenu();
    }

    private void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.LevelChanged -= OnLevelChanged;
            GameManager.instance.GameOvered -= OnGameOvered;
        }
    }

    private void OnLevelChanged(object sender, EventArgs e)
    {
        UpdateLevelText();
    }

    private void UpdateLevelText()
    {
        levelText.text = "LEVEL " + (GameManager.instance.level);
    }

    private void OnGameOvered(object sender, EventArgs e)
    {
        UpdateGameOverScore();

        gameOverText.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
    }

    private void UpdateGameOverScore()
    {
        if (ScoreData.instance.isBestScore)
            bestScoreText.text = "NEW RECORD !";
        else
            bestScoreText.text = "Best Score: " + ScoreData.instance.bestScoreTime + " sec, " + ScoreData.instance.bestScoreLevel + " lvl";

        yourScoreText.text = "Your Score: " + (int)Math.Floor(GameManager.instance.gameDuration) + " sec, " + GameManager.instance.level + " lvl";
    }

    public void PauseMenu()
    {
        MenuSound();

        if (pauseMenu.activeSelf)
        {
            GameManager.instance.state = GameManager.GameState.Play;
            GameManager.instance.timeScale = 1;
            pauseMenu.SetActive(false);
            GameManager.instance.gameMusic.UnPause();
        }
        else
        {
            GameManager.instance.state = GameManager.GameState.Pause;
            GameManager.instance.timeScale = 0;
            pauseMenu.SetActive(true);
            GameManager.instance.gameMusic.Pause();
        }
    }

    public void Continue()
    {
        MenuSound();

        gameOverText.SetActive(false);
        gameOverMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        MenuSound();

        GameManager.instance.state = GameManager.GameState.Play;
        GameManager.instance.timeScale = 1;
        GameManager.instance.gameMusic.UnPause();
        pauseMenu.SetActive(false);
    }

    public void RestartGame()
    {
        StartGameUI();

        GameManager.instance.RestartGame();
    }

    public void ToMenu()
    {
        MenuSound();

        GameManager.instance.StopGame();
        GameManager.instance.state = GameManager.GameState.StartMenu;
        SceneLoader.instance.LoadStartMenuScene();
    }

    public void ExitGame()
    {
        exitSound.Play();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    private void MenuSound()
    {
        menuSound.Play();
    }

}
