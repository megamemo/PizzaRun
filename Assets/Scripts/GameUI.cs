using UnityEngine;
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
    [SerializeField] private TextMeshProUGUI bestScoreGOText;
    [SerializeField] private TextMeshProUGUI yourScoreText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Button pauseButton;


    private void Awake()
    {
        SetBestScoreText();    
    }

    private void Start()
    {
        StartGame();

        GameManager.instance.LevelChanged += OnLevelChanged;
        GameManager.instance.GameOvered += OnGameOvered;
    }

    private void Update()
    {
        CheckUserInput();
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

    private void OnGameOvered(object sender, EventArgs e)
    {
        UpdateGameOverScore();

        gameOverText.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
    }

    public void PauseMenuUI()
    {
        MenuSound();

        if (pauseMenu.activeSelf)
            UnPauseGame();
        else
            PauseGame();
    }

    public void ContinueUI()
    {
        MenuSound();

        gameOverText.SetActive(false);
        gameOverMenu.SetActive(true);
    }

    public void ResumeGameUI()
    {
        MenuSound();
        UnPauseGame();
    }

    public void RestartGameUI()
    {
        StartGame();

        GameManager.instance.RestartGame();
    }

    public void BackToMenuUI()
    {
        MenuSound();

        GameManager.instance.StopGame();
        GameManager.instance.gameState = GameManager.GameState.MainMenu;
        SceneLoader.instance.LoadMainMenuScene();
    }

    public void ExitGameUI()
    {
        exitSound.Play();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    private void StartGame()
    {
        UpdateLevelText();

        pauseMenu.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        gameOverMenu.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);
    }

    private void CheckUserInput()
    {
        if (GameManager.instance.gameState == GameManager.GameState.GameOver)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
            PauseMenuUI();
    }

    private void SetBestScoreText()
    {
        bestScoreText.text = "Best Score: " + ScoreData.instance.bestScoreTime + " sec, " + ScoreData.instance.bestScoreLevel + " lvl";
    }

    private void UpdateLevelText()
    {
        levelText.text = "LEVEL " + (GameManager.instance.level);
    }

    private void UpdateGameOverScore()
    {
        if (ScoreData.instance.isNewBestScore)
            bestScoreGOText.text = "NEW RECORD !";
        else
            bestScoreGOText.text = "Best Score: " + ScoreData.instance.bestScoreTime + " sec, " + ScoreData.instance.bestScoreLevel + " lvl";

        yourScoreText.text = "Your Score: " + (int)Math.Floor(GameManager.instance.gameDuration) + " sec, " + GameManager.instance.level + " lvl";
    }

    private void MenuSound()
    {
        menuSound.Play();
    }

    private void PauseGame()
    {
        GameManager.instance.PauseGame();

        pauseMenu.SetActive(true);
    }

    private void UnPauseGame()
    {
        GameManager.instance.UnPauseGame();

        pauseMenu.SetActive(false);
    }

}
