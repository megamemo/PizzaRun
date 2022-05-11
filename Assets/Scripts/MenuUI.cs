using TMPro;
using UnityEngine;
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
    [SerializeField] private AudioSource menuSound;
    [SerializeField] private AudioSource exitSound;


    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            BackToMenu();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Scores()
    {
        MenuSound();
        mainMenu.SetActive(false);

        for (int i = 0; i < ScoreData.instance.scoreArrayLenght; i++)
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
