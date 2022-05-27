using TMPro;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class StartMenuUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] scoreTexts;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject scores;
    [SerializeField] private AudioSource menuMusic;
    [SerializeField] private AudioSource menuSound;
    [SerializeField] private AudioSource exitSound;


    private void Start()
    {
        menuMusic.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            BackToMenu();
    }

    public void StartGame()
    {
        menuMusic.Stop();

        SceneLoader.instance.LoadGameScene();
        GameManager.instance.StartGame();
    }

    public void Scores()
    {
        MenuSound();
        mainMenu.SetActive(false);

        for (int i = 0; i < ScoreData.instance.scoreArrayLength; i++)
        {
            scoreTexts[i].text = (i + 1) + ": " + ScoreData.instance.scoreTimes[i] + " sec, " + ScoreData.instance.scoreLevels[i] + " lvl";
        }

        scores.SetActive(true);
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

    public void BackToMenu()
    {
        MenuSound();
        scores.SetActive(false);
        mainMenu.SetActive(true);
    }

    private void MenuSound()
    {
        menuSound.Play();
    }

}
