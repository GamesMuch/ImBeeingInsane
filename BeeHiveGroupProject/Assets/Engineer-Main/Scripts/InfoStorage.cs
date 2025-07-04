using System.Collections.Generic;
using UnityEngine;

public class InfoStorage : MonoBehaviour
{
    [System.Serializable]
    public struct PlayerAvatar
    {
        public Sprite MouthOpen;
        public Sprite MouthClose;
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


    public PlayerAvatar playerAvatar;

    [System.Serializable]
    public struct Scene
    {
        public string MainMenuName;
        public string GameSceneName;
        public string FailSceneName;
        public string WinSceneName;
        public string FightSceneName;
    }
    public Scene scenes;





    public string DoorID;
    public GameObject currentDoor;

    public GameObject player;


    Transform nullSpawn;

    public GameObject cam;

    public GameObject blinkItem;

    List<DoorScript> doors = new();

    List<DialogueBox> dialogueBoxes = new();



    public bool InDialogue;

    float time;
    bool canDoor;
    float doorDist;

    public bool PollenAquired;
    public bool GotScreech;
    public bool GotHornet;
    public bool KnowsHornet;





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
        GetVariables();
    }
    private void Update()
    {
        if (!canDoor)
        {

            time += Time.deltaTime;
            if (time > 1)
            {
                canDoor = true;
            }
        }
        if (canDoor)
        {
            time = 0;
        }
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
            foreach (DoorScript door in doors)
            {
                if (door.doorID == DoorID && currentDoor.name != door.gameObject.name)
                {
                    print(currentDoor.name);
                    print(door.gameObject.name);
                    door.MovePlayerHere();
                    break;
                }
            }

            PlayMusic("Door");



        }
    }

    public void PlayMusic(string song)
    {
        if (song == "Queen")
        {
            musicPlayer.clip = allMusic.QueenSound;
            musicPlayer.Play();
        }
        else if (song == "Door")
        {
            musicPlayer.clip = allMusic.DoorSound;
            musicPlayer.Play();
        }
        else if (song == "Talk")
        {
            musicPlayer.clip = allMusic.TypeSound;
            musicPlayer.Play();
        }
        else if (song == "Screech")
        {
            musicPlayer.clip = allMusic.ScreechSound;
            musicPlayer.Play();
        }
        else
        {
            Debug.LogWarning("No audio selected!");
        }
    }
    public void StopMusic()
    {
        musicPlayer.Stop();
    }

    public void HardReset()
    {
        PollenAquired = false;
        GotScreech = false;
        GotHornet = false;
        KnowsHornet = false;
    }
    public void SoftReset()
    {
        PollenAquired = false;
        GotScreech = false;
        GotHornet = false;
    }
    public void GetVariables()
    {
        DoorScript[] temp = FindObjectsByType<DoorScript>(FindObjectsSortMode.None);
        foreach (DoorScript obj in temp)
        {
            if (!doors.Contains(obj))
            {
                doors.Add(obj);
            }
        }
        DialogueBox[] temp2 = FindObjectsByType<DialogueBox>(FindObjectsSortMode.None);
        foreach (DialogueBox obj in temp2)
        {
            if (!dialogueBoxes.Contains(obj))
            {
                dialogueBoxes.Add(obj);
            }
        }
        if (blinkItem != null)
        {
            blinkItem.SetActive(true);
            blinkItem.SetActive(false);
        }
        musicPlayer = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        blinkItem = GameObject.Find("JUSTBLINKS");
    }
}
