using UnityEngine;

public class NormalScene : MonoBehaviour
{
    public void FightScene()
    {
        SceneSwapper.Instance.FightScene();
    }
    public void Death()
    {
        SceneSwapper.Instance.FailScene();
    }
    public void Win()
    {
        SceneSwapper.Instance.WinScene();
    }
    public void Menu()
    {
        SceneSwapper.Instance.StartScene();
    }
}
