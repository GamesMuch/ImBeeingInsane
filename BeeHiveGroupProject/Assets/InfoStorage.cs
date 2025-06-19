using UnityEngine;
using UnityEngine.UI;

public class InfoStorage : MonoBehaviour
{
    [System.Serializable]
    public struct PlayerAvatar
    {
        public Sprite MouthOpen;
        public Sprite MouthClose;
    }
    public PlayerAvatar playerAvatar;

    public GameObject item;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        item.SetActive(true);
        item.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
