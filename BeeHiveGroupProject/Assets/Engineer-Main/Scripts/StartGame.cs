using UnityEngine;

public class StartGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject DialogueBox;
    void Start()
    {
        print("Pens");
        InfoStorage.Instance.GetVariables();
    }

}
