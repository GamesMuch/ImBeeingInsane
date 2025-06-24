using UnityEditor;
using UnityEditor.SearchService;
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
    [System.Serializable]
    public struct allLevels
    {
        public SceneAsset Scene1;
        public SceneAsset Scene2;
        public SceneAsset Scene3;
    }

    public allLevels Scenes;

    public PlayerAvatar playerAvatar;

   

    public int DoorID;

    public static InfoStorage Instance;
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
}
