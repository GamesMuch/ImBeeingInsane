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

    Dictionary<int, int> choicesList = new Dictionary<int, int>();

    [Tooltip("How many characters per part get revealed")]
    public int lettersPerBatch = 3;
    [Tooltip("How fast the parts get revealed")]
    public float talkSpeed = 1;

    //Bools for talk logic
    bool inDialogue = false;
    bool NextLine = true;
    bool Tapped;
    bool NoOption = true;

    //Some variables
    int line;           
    int QuestionNr;      
    int CurrentQuestion; 

    //Just timechecker
    float time;

    #region SourceCode

    public void OnTalk()
    {
        inDialogue = true;
        Player.agent.isStopped = true;
        choicesList.Clear();
        line = 0;

        partsOfLines.Clear();
        D.Clear();
        

        
        InitializeDialogue();

        TryTalk();
       
    }
    void TryTalk()
    {
        if (inDialogue)
        {
            Player.agent.isStopped = true;
            Player.GetComponent<PlayerMovement>().canMove = false;

            DialogueObject.SetActive(true);

            if (Tapped && NextLine && NoOption)
            {
                partsOfLines.Clear();
                D.Clear();
                

                InitializeDialogue();

                if (line < D.Count)
                {

                    Tapped = false;
                    NextLine = false;

                    int nr = D[line].Item1;
                    string nextLine = D[line].Item2;
                    
                    Talk(nr, nextLine);
                    
                }
                else
                {
                    DialogueEnd();
                }
            }
        }
    }
    void Talk(int nr, string sentence)
    {
        partsOfLines.Clear();
        TextBox.text = "";
        if (nr == 0 || nr == 1)
        {
            FirstOptionBox.SetActive(false);
            SecondOptionBox.SetActive(false);

            for (int i = 0; i < sentence.Length; i += lettersPerBatch)
            {
                int size = Mathf.Min(lettersPerBatch, sentence.Length - i);
                partsOfLines.Add(sentence.Substring(i, size));
                
            }
            if (line + 1 < D.Count && D[line + 1].Item1 == 2)
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
        
        else if (nr == 3)
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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Tapped = true;
            TryTalk();
        }
        else
        {
            Tapped = false;
        }
    }


    #region Choosing
    public void FirstOption()
    {
        choicesList[QuestionNr]= 1;
        NoOption = true;
        FirstOptionBox.SetActive(false);
        SecondOptionBox.SetActive(false);
        Tapped = true;
        NextLine = true;

        TryTalk();
    }
    public void SecondOption()
    {
        choicesList[QuestionNr]= 2;
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

    void Q(string text) => D.Add((2, text));

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

    void End() => D.Add((3,"End"));
    #endregion

    #region Flavor
    IEnumerator AddToText(int nr)
    {
        foreach (string s in partsOfLines) {
            TextBox.text += s;

            yield return new WaitForSeconds(1f / (talkSpeed * partsOfLines.Count));
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
        NextLine = true;


    }
    #endregion

    void InitializeDialogue() {
        switch (ThisNPC)
        {
            case NPC.none:

                Debug.LogError("There is no script for this yet");

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

        Q("Thats so cool!");
        Q("Wow!");

        QuestionNr = 12;

        if (ifX(QuestionNr) == 1)
        {
            N("Oh thank you thank you, i feel honored");

            P("No problem, take care!");

            N("You too! Bye!");

            End();
        }
        if (ifX(QuestionNr) == 2)
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

        Q("Yes i have");
        Q("No, i have not");

        QuestionNr = 1;
        if (ifX(QuestionNr) == 1)
        {
            N("Thats amazing");
            N("Thats amazing2");
            Q("Yeah");
            Q("No fuck you!");
            QuestionNr = 2;
            if (ifX(QuestionNr) == 1)
            {
                N("Fuck you");
            }
            if (ifX(QuestionNr) == 2)
            {
                N("Yaaay");
            }
            N("Thats amazing3");
        }
        else if (ifX(QuestionNr) == 2)
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