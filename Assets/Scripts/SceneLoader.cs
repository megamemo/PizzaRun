using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance { get; private set; }


    private void Awake()
    {
        InstanciateSceneLoader();

        SceneManager.LoadScene("StartMenu", LoadSceneMode.Additive);
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

    public void LoadStartMenuScene()
    {
        SceneManager.UnloadSceneAsync("Game");
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Additive);
    }

    public void LoadGameScene()
    {
        SceneManager.UnloadSceneAsync("StartMenu");
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
    }

}
