using UnityEngine;

public class FightBTNS : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Fail()
    {
        SceneSwapper.Instance.FailScene();
    }

    void Succeed()
    {
        SceneSwapper.Instance.MainScene();
    }

}
