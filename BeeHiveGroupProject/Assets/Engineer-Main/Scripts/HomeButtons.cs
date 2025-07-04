using UnityEngine;

public class HomeButtons : MonoBehaviour
{
    public void StartScene()
    {
        InfoStorage.Instance.HardReset();
        SceneSwapper.Instance.MainScene();
    }
    public void Exit()
    {
        //Idk man
    }
}
