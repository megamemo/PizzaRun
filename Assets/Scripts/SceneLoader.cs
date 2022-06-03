using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance { get; private set; }


    private void Awake()
    {
        InstanciateSceneLoader();

        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
    }

    public void LoadMainMenuScene()
    {
        SceneManager.UnloadSceneAsync("Game");
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
    }

    public void LoadGameScene()
    {
        SceneManager.UnloadSceneAsync("MainMenu");
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
    }

    private void InstanciateSceneLoader()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

}
