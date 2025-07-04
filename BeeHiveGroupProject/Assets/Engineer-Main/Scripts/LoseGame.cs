using UnityEngine;

public class LoseGame : MonoBehaviour
{
    public void Restart()
    {
        InfoStorage.Instance.SoftReset();
        SceneSwapper.Instance.MainScene();
    }
    public void MainMenu()
    {
        SceneSwapper.Instance.StartScene();
    }
}
