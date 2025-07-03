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
        SceneManager.LoadScene(InfoStorage.Instance.scenes.Home.name);
    }
    public void FailScene()
    {
        SceneManager.LoadScene(InfoStorage.Instance.scenes.Fail.name);
    }
    public void WinScene()
    {
        SceneManager.LoadScene(InfoStorage.Instance.scenes.Win.name);
    }
    public void MainScene()
    {
        SceneManager.LoadScene(InfoStorage.Instance.scenes.Main.name);
    }
    public void FightScene()
    {
        SceneManager.LoadScene(InfoStorage.Instance.scenes.Fight.name);
    }
}
