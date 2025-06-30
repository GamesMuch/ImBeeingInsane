using System.Collections.Generic;
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

    public GameObject player;

    public Transform nullSpawn;

    public GameObject cam;

    public GameObject blinkItem;

    public List<DoorScript> stairs = new();

    public List<DialogueBox> dialogueBoxes = new();

    int answerNr;

    public bool InDialogue;



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
    void Start()
    {
        blinkItem.SetActive(true);
        blinkItem.SetActive(false);

    }
    public void FirstAnswer()
    {
        foreach (DialogueBox d in dialogueBoxes)
        {
            if (Vector3.Distance(player.transform.position, d.gameObject.transform.position) <= 2f)
            {
                d.choicesList[d.AnswerID] = 1;
                d.DidOption();
                break;
            }
        }
    }
    public void SecondAnswer()
    {
        foreach (DialogueBox d in dialogueBoxes)
        {
            if (Vector3.Distance(player.transform.position, d.gameObject.transform.position) <= 2f)
            {
                d.choicesList[d.AnswerID] = 2;
                d.DidOption();
                break;
            }
        }
    }
}
