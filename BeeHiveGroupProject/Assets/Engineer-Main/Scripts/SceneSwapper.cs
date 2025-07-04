using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwapper : MonoBehaviour
{
    public static SceneSwapper Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void StartScene()
    {
        SceneManager.LoadScene(InfoStorage.Instance.scenes.MainMenuName);
    }
    public void FailScene()
    {
        SceneManager.LoadScene(InfoStorage.Instance.scenes.FailSceneName);
    }
    public void WinScene()
    {
        SceneManager.LoadScene(InfoStorage.Instance.scenes.WinSceneName);
    }
    public void MainScene()
    {
        SceneManager.LoadScene(InfoStorage.Instance.scenes.GameSceneName);
    }
    public void FightScene()
    {
        SceneManager.LoadScene(InfoStorage.Instance.scenes.FightSceneName);
    }
}
