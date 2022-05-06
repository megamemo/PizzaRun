using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MenuUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] scoreTexts;

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject scores;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMenu();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Scores()
    {
        mainMenu.SetActive(false);

        for (int i = 0; i < ScoreData.instance.scoreArrayLenght; i++)
        {
            scoreTexts[i].text = (i + 1) + ": " + ScoreData.instance.scoreTimes[i] + " sec, " + ScoreData.instance.scoreLevels[i] + " lvl";
        }

        scores.SetActive(true);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void BackToMenu()
    {
        scores.SetActive(false);
        mainMenu.SetActive(true);
    }
}
