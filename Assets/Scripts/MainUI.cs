using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

[DefaultExecutionOrder(1000)]
public class MainUI : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private AudioSource menuSound;
    [SerializeField] private AudioSource exitSound;


    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseMenu();
    }

    public void PauseMenu()
    {
        MenuSound();

        if (pauseMenu.activeSelf)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            GameManager.instance.gameMusic.UnPause();
        }
        else
        {
            Time.timeScale = 0;
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
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        GameManager.instance.gameMusic.UnPause();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToMenu()
    {
        MenuSound();
        SceneManager.LoadScene(0);
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
