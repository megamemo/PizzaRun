using TMPro;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] scoreTexts;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject scores;
    [SerializeField] private AudioSource menuSound;
    [SerializeField] private AudioSource exitSound;


    private void Start()
    {
        GameManager.instance.SetMainMenuState();
    }

    private void Update()
    {
        CheckUserInput();
    }

    public void StartGameUI()
    {
        SceneLoader.instance.LoadGameScene();
        GameManager.instance.StartGame();
    }

    public void ScoresUI()
    {
        MenuSound();

        mainMenu.SetActive(false);

        for (int i = 0; i < ScoreData.instance.scoreArrayLength; i++)
        {
            scoreTexts[i].text = (i + 1) + ": " + ScoreData.instance.scoreTimes[i] + " sec, " + ScoreData.instance.scoreLevels[i] + " lvl";
        }

        scores.SetActive(true);
    }

    public void BackToMenuUI()
    {
        MenuSound();
        scores.SetActive(false);
        mainMenu.SetActive(true);
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

    private void CheckUserInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            BackToMenuUI();
    }

    private void MenuSound()
    {
        menuSound.Play();
    }

}
