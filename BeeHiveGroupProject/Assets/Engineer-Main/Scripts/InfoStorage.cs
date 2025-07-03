using System.Collections.Generic;
using Unity.VisualScripting;
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

    [System.Serializable]
    public struct Music
    {
        public AudioClip QueenSound;
        public AudioClip TypeSound;
        public AudioClip ScreechSound;
        public AudioClip DoorSound;
    }
    public Music allMusic;

    AudioSource musicPlayer;
    public allLevels Scenes;

    public PlayerAvatar playerAvatar;


   

   

    public int DoorID;
    public GameObject currentDoor;

    public GameObject player;
    

    public Transform nullSpawn;

    public GameObject cam;

    public GameObject blinkItem;

    public List<DoorScript> stairs = new();

    public List<DialogueBox> dialogueBoxes = new();

    int answerNr;

    public bool InDialogue;

    float time;
    public bool canDoor;
    float doorDist;

    public bool PollenAquired;
    public bool GotHornet;





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
        musicPlayer = GetComponent<AudioSource>();
    }
    private void Update()
    {
        //if (!canDoor)
        //{
            
        //    time += Time.deltaTime;
        //    if (time > 1)
        //    {
        //        canDoor = true;
        //    }
        //}
        //if (canDoor)
        //{
        //    time = 0;
        //}
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
    public void OtherDoor() 
    {
        if (canDoor == true)
        {
            canDoor = false;
            foreach (DoorScript door in stairs)
            {
                if (door.doorID == DoorID && currentDoor.name != door.gameObject.name)
                {
                    print (currentDoor.name);
                    print(door.gameObject.name);
                    player.transform.position = door.transform.position;
                    
                }
            }
            
            PlayMusic("Door");

            print("Jumped!");
            
        }
    }

    public void PlayMusic(string song)
    {
        if (song == "Queen")
        {
            musicPlayer.clip = allMusic.QueenSound;
            musicPlayer.Play();
        }
        if (song == "Door")
        {
            musicPlayer.clip = allMusic.DoorSound;
            musicPlayer.Play();
        }
        if (song == "Talk")
        {
            musicPlayer.clip = allMusic.TypeSound;
            musicPlayer.Play();
        }
        if (song == "Screech")
        {
            musicPlayer.clip = allMusic.ScreechSound;
            musicPlayer.Play();
        }
        if (song == null)
        {
            Debug.LogWarning("No audio selected!");
        }
    }
    public void StopMusic()
    {
        musicPlayer.Stop();
    }
}
