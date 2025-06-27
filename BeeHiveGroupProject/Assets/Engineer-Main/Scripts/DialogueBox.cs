using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    [System.Serializable]
    public struct talkAvatar
    {
        public Sprite MouthOpen;
        public Sprite MouthClosed;
    }

    public talkAvatar NPCAvatar;

    [System.Serializable]
    public struct talkAvatar2
    {
        public Sprite MouthOpen2;
        public Sprite MouthClosed2;
    }

    public talkAvatar NPCAvatar2;

    [Header("Important Objects")]
    public InfoStorage Info;
    public PlayerMovement Player;
    public Image DialogueImage;
    public TextMeshProUGUI TextBox = null;

    [Space(10)]
    [Header("Dialogue Boxes")]
    public GameObject DialogueObject;
    public GameObject FirstOptionBox;
    public GameObject SecondOptionBox;

    
    public enum NPC { none, worker, grandpa, queen, trauma, guard, ass, sleepGuard, test, questions }
    [Tooltip("Which NPC this is")]
    public NPC ThisNPC = NPC.none;

    
    List<(int, string)> D = new List<(int, string)>(); 
    List<string> partsOfLines = new List<string>();

    [HideInInspector]
    public Dictionary<int, int> choicesList = new Dictionary<int, int>();

    [Tooltip("How many characters per part get revealed")]
    public int lettersPerBatch = 3;
    [Tooltip("How fast the parts get revealed")]
    public float talkSpeed = 1;

    //Bools for talk logic
    public bool inDialogue = false;
    bool NextLine = true;
    bool Tapped;
    bool NoOption = true;

    //Some variables
    int line;
    [HideInInspector]
    public int AnswerID;      
    int CurrentQuestion; 

    //Just time checker
    float time;

    #region SourceCode

    public void OnTalk()
    {
        line = 0;
        Tapped = true;
        inDialogue = true;
        Player.agent.isStopped = true;
        Player.canMove = false;

        choicesList.Clear();
        

        partsOfLines.Clear();
        D.Clear();
        

        
        InitializeDialogue();

        StartCoroutine(waitAFrame());
       
    }
    IEnumerator waitAFrame()
    {
        yield return null;
        print("Gwork");
        TryTalk();
    }
    void TryTalk()
    {
        Debug.Log("Grink");
        if (inDialogue)
        {
            Debug.Log("Grockle");
            Player.agent.isStopped = true;
            Player.GetComponent<PlayerMovement>().canMove = false;

            DialogueObject.SetActive(true);

            if (Tapped && NextLine && NoOption)
            {
                Tapped = false;
                Debug.Log("Vesuvius");
                

                partsOfLines.Clear();
                D.Clear();
                StopCoroutine("TalkAvatar");

                InitializeDialogue();


                if (line < D.Count)
                {

                    
                    NextLine = false;

                    int nr = D[line].Item1;
                    string nextLine = D[line].Item2;
                    
                    Talk(nr, nextLine);
                    
                }
                else
                {
                    DialogueEnd();
                    Debug.Log("Bread in france");
                }
            }
            print("springus");
        }
    }
    void Talk(int nr, string sentence)
    {
        partsOfLines.Clear();
        TextBox.text = "";
        if (nr == 0 || nr == 1 || nr == 2)
        {
            FirstOptionBox.SetActive(false);
            SecondOptionBox.SetActive(false);

            for (int i = 0; i < sentence.Length; i += lettersPerBatch)
            {
                int size = Mathf.Min(lettersPerBatch, sentence.Length - i);
                partsOfLines.Add(sentence.Substring(i, size));
                
            }
            if (line + 1 < D.Count && D[line + 1].Item1 == 3)
            {
                TextMeshProUGUI text1 = FirstOptionBox.GetComponentInChildren<TextMeshProUGUI>();
                TextMeshProUGUI text2 = SecondOptionBox.GetComponentInChildren<TextMeshProUGUI>();
                text1.text = "";
                text2.text = "";

                FirstOptionBox.SetActive(true);
                SecondOptionBox.SetActive(true);

                NoOption = false;
                string option1 = D[line + 1].Item2;
                string option2 = D[line + 2].Item2;
                line += 2;
                print(line);

                SetUpOptions(option1, option2);
            }
            
            
            
        }
        
        else if (nr == 4)
        {
            DialogueEnd();
            Debug.Log("IT GOTTA STOP");
        }
        StartCoroutine(AddToText(nr));
        StartCoroutine(TalkAvatar(nr));
        line++;


    }

    void SetUpOptions(string option1, string option2)
    {
        TextMeshProUGUI option1Txt = FirstOptionBox.GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI option2Txt = SecondOptionBox.GetComponentInChildren<TextMeshProUGUI>();

        option1Txt.text = option1;
        option2Txt.text = option2;
    }
    private void Update()
    {
        if (inDialogue)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Tapped = true;
                TryTalk();
            }
        }
    }


    #region Choosing


    public void DidOption()
    {
        print("AGONY");
        NoOption = true;
        FirstOptionBox.SetActive(false);
        SecondOptionBox.SetActive(false);
        Tapped = true;
        NextLine = true;

        TryTalk();
    }

    void DialogueEnd()
    {
        inDialogue = false;
        NextLine = true;
        NoOption = true;


        DialogueObject.SetActive(false);
        FirstOptionBox.SetActive(false);
        SecondOptionBox.SetActive(false);
        Player.agent.isStopped = false;

        Player.GetComponent<PlayerMovement>().canMove = true;

    }
    #endregion

    #region TalkCode
    void P(string text) => D.Add((0, text));
    void N(string text) => D.Add((1, text));
    void S(string text) => D.Add((2, text));

    void A(string text) => D.Add((3, text));

    int ifX(int nr)
    {
        if (choicesList.ContainsKey(nr))
        {
            return choicesList[nr];
        }
        else
        {
            return 0;
        }
    }

    void End() => D.Add((4,"End"));
    #endregion

    #region Flavor
    //StopCoroutine("AddToText");
    IEnumerator AddToText(int nr)
    {
        foreach (string s in partsOfLines) {

            if (string.IsNullOrEmpty(s)) yield break;


            TextBox.text += s;
            if (Tapped == false)
            {
                yield return new WaitForSeconds(1f / (talkSpeed * partsOfLines.Count));
            }
            if (Tapped == true)
            {
                yield return null;
            }
        }
        
    }
    IEnumerator TalkAvatar(int nr)
    {
        
        if (nr == 0) {
            for (int i = 0; i < talkSpeed; i++)
            {
                DialogueImage.sprite = Info.playerAvatar.MouthOpen;
                //DialogueImage.color = new Color(1, 0, 1);

                yield return new WaitForSeconds(1f / (talkSpeed * 2));

                
                DialogueImage.sprite = Info.playerAvatar.MouthClose;
                //DialogueImage.color = new Color(0, 1, 0);
                yield return new WaitForSeconds(1f / (talkSpeed * 2));
            }
            
            
        }
        if (nr == 1)
        {
            for (int i = 0; i < talkSpeed; i++)
            {
                DialogueImage.sprite = NPCAvatar.MouthOpen;
                //DialogueImage.color = new Color(1, 0, 1);
                yield return new WaitForSeconds(1f / (talkSpeed * 2));
                DialogueImage.sprite = NPCAvatar.MouthClosed;
                //DialogueImage.color = new Color(0, 0, 1);
                yield return new WaitForSeconds(1f / (talkSpeed * 2));
            }
        }
        if (nr == 2)
        {
            for (int i = 0; i < talkSpeed; i++)
            {
                DialogueImage.sprite = NPCAvatar2.MouthOpen;
                //DialogueImage.color = new Color(1, 0, 1);
                yield return new WaitForSeconds(1f / (talkSpeed * 2));
                DialogueImage.sprite = NPCAvatar2.MouthClosed;
                //DialogueImage.color = new Color(0, 0, 1);
                yield return new WaitForSeconds(1f / (talkSpeed * 2));
            }
        }
        NextLine = true;


    }
    #endregion

    void InitializeDialogue() {
        switch (ThisNPC)
        {
            case NPC.none:

                Debug.LogError("There is no script for this");

                break;

            case NPC.worker:
                WorkerDialogue();
                break;

            case NPC.grandpa:

                OldManDialogue();

                break;

            case NPC.queen:
                QueenDialogue();
                break;

            case NPC.test:


                TestDialogue();
                break;
            case NPC.ass:
                AssistantDialogue();
                break;

            case NPC.trauma:
                TraumaDialogue();
                break;
            case NPC.guard:
                GuardDialogue();
                break;
            case NPC.sleepGuard:
                SleepingDialogue();
                break;

            case NPC.questions:
                QuestionDialogue();
                break;

        }
    
    
    }
    //etc
    #endregion
    void TestDialogue()
    {
        N("Hello there");

        P("Who are you?");

        N("Im just a test script of this amazing dialogue system");

        A("Thats so cool!");
        A("Wow!");

        AnswerID = 12;

        if (ifX(AnswerID) == 1)
        {
            N("Oh thank you thank you, i feel honored");

            P("No problem, take care!");

            N("You too! Bye!");

            End();
        }
        if (ifX(AnswerID) == 2)
        {
            N("Dont worry bout it sport, thats for later");

            P("Alright thanks");

            N("Take care now!");

            End();
        }

        End();
    }
    void QuestionDialogue()
    {
        N("Hey");

        P("Hello again");

        N("Have you seen John?");

        A("Yes i have");
        A("No, i have not");

        AnswerID = 1;
        if (ifX(AnswerID) == 1)
        {
            N("Thats amazing");
            N("According to all known laws of aviation, there is no way a bee should be able to fly, its wings are too small");
            A("Yeah");
            A("YeahNr2");
            AnswerID = 2;
            if (ifX(AnswerID) == 1)
            {
                N("Fuck you");
            }
            if (ifX(AnswerID) == 2)
            {
                N("Yaaay");
            }
            N("Thats amazing3");
        }
        else if (ifX(AnswerID) == 2)
        {
            N("What a bummer");
            N("What a bummer2");
            End();
        }
    }


    void WorkerDialogue()
    {

    }
    void QueenDialogue()
    {

    }
    void AssistantDialogue()
    {

    }
    void TraumaDialogue()
    {

    }
    void OldManDialogue()
    {

    }
    void GuardDialogue()
    {

    }
    void SleepingDialogue()
    {

    }
}