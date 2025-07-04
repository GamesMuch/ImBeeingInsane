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

    
    public enum NPC { none, drunk, grandpa, queen, trauma, guard, ass, sleepGuard, test, questions }
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
    public bool IsScreeched;

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
        choicesList.Clear();


        partsOfLines.Clear();
        D.Clear();

        line = 0;
        Tapped = true;
        inDialogue = true;
        Player.agent.isStopped = true;
        Player.canMove = false;


        

        
        InitializeDialogue();

        StartCoroutine(waitAFrame());
       
    }
    IEnumerator waitAFrame()
    {
        yield return null;
        
        TryTalk();
    }
    void TryTalk()
    {
        
        if (inDialogue)
        {

            print("Tapped = " + Tapped + " NextLine = " + NextLine + " No Option = " + NoOption);
            Player.agent.isStopped = true;
            Player.GetComponent<PlayerMovement>().canMove = false;

            DialogueObject.SetActive(true);

            if (Tapped && NextLine && NoOption)
            {
                Tapped = false;
               
                

                partsOfLines.Clear();
                D.Clear();
                StopCoroutine("TalkAvatar");

                InitializeDialogue();


                if (line < D.Count)
                {

                    print(line);
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
        
        NoOption = true;
        FirstOptionBox.SetActive(false);
        SecondOptionBox.SetActive(false);
        Tapped = true;
        NextLine = true;

        print("Did the option idk what to tell you mate");

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
        InfoStorage.Instance.PlayMusic("Talk");
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
        InfoStorage.Instance.StopMusic();
        
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

            case NPC.drunk:
                DrunkDialogue();
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
    #region Dialogue
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



    void QueenDialogue()
    {
        N("Why have you come to disturb me, worker?");
        P("Your Buzzyness, hornets are coming to attack the hive!");
        N("Oh my little one, what an imagination do you have.");
        P("Your Majestbee it’s true!");
        N("Why would I believe a lowly bee like yourself?");
        P("I can prove it!");

        // [Show evidence] — narrative action (keep as a comment or turn into a function if you handle events)

        N("Well, this certainly casts a different light on things. How did you come to possess this?");
        P("Your Majestbee, earlier a hornet tried to break in through one of the cracks in the wall.");
        N("And you… you defeated it?");
        P("With help of the guards…");
        N("You’ve done well, I’ll warn the workers and we’ll defend ourselves! Good job");
        P("Thank you, your MajestBee");
        End();
        QueenScreech();
    }
    void AssistantDialogue()
    {
        N("What is a little worker like you doing in the royal quarters of Her MajestBee?");
        P("I have an urgent message for the QUEEN!");
        N("Everybee has an urgent message for the QUEEN these days. What, pray tell, makes yours any different?");
        P("It’s about the safety of the hive!");
        N("The safety of the hive? How very… ambitious of you. If you expect to bother her Majestbee, you’ll first need to prove you speak of truth.");
        P("I- I have proof here!");
        N("How did you- Hmpf. Very well, I suppose I can grant you, small bee, the rare honor of an audience. But only this once.");
        P("Thank you.");
        N("Do try not to waste her MajestBee’s time.");
        End();
    }
    void DrunkDialogue()
    {
        if (InfoStorage.Instance.PollenAquired == false)
        {
            P("Buzz! BUZZBEE. I need your help!");
            N("*hiccup* Whatcha need, honeybun?");
            P("Well… PTSBEE is buzzed out of his mind. He told me to find you.");
            N("And why is that my problem?");
            P("I…. Can’t you help me?");
            N("Tsss, as if I’d ever, *hiccup* help anyone for free.");
            P("What do you want?");
            N("You little... honeybun... can’t get what I want.");
            P("It’s at least worth the try!");
            N("You know what… *hiccup* I am running low on pollen and, *hiccup* I’d like to restash.");
            P("Why ask me? Can’t you just ask them for more?");
            N("….");
            N("I’m not really allowed anymore…");
            P("Ohh….");
            P("Where can I find the pollen?");
            N("It’s downstairs in the storage. But you gotta pass ANTO and PHILA.");
            P("If I get it for you, will you help me?");
            N("*laughs* If you can get it for me, I’ll help ya with ya trouble.");
            N("Yeah… so you get it for me. And be quick.");
            End();
        }

        else if (InfoStorage.Instance.PollenAquired == true)
        {
            N("Finally lil’ HoneyBun. *hiccup* could it have taken ya any longer.");
            P("I’m sorry, there was some… trouble.");
            N("At least ya got tha deed done ey.");
            P("Can you please help me now?");
            N("Aight, aight….");
            P("…");
            N("What did ya need help with again?");
            P("I need to help PTSBEE! She’s in shock!");
            N("Yeah right. You need to use yer high pitch on her.");
            P("My… My what?");
            N("Yer high pitch. Repeat after me!");
            End();
        }

    }
    void TraumaDialogue()
    {
        if (IsScreeched == false)
        {
            P("PTSBEE! What’s wrong?");
            N("Coming... *buzz*");
            P("What’s coming?");
            N("*buzz* They’re coming...! *buzz*");
            P("He’s too traumatized. I can’t get through to him.");
            N("Find…… BUZZBEE");
            P("Okay, okay. I’ll find him. Just, stay here okay?");
            End();
        }
        if (IsScreeched == true)
        {
            P("PTSBEE, are you okay?");
            N("SKIBEE! I- Thank you...");
            P("What got you stinged?");
            N("Oh, SKIBEE it’s horrible. Just horrible. The worst thing ever. I can’t even Beelieve it. I-");
            P("Focus, what happened?");
            N("The hornets. It’s the hornets. A hundred, no no, a thousands! A whole army is on their way!");
            P("To where?? The hive?");
            N("Yes. Oh my Bee. We’re all gonna die!");
            P("Buzz up. We’re not, okay? We just gotta warn the QUEEN.");
            N("How? We’re just some lowly worker bees. We’re the lowest rank in the hive! They’re not gonna listen to you.");
            P("If we prove to the QUEEN there is a threat, she’ll have to believe us.");
            N("But how would we-");

            //Play hornet sound?

            P("The hornet! If we can bring him to the QUEEN she’ll know.");
            End();
        }
    }
    void OldManDialogue()
    {

    }
    void GuardDialogue()
    {
        if (InfoStorage.Instance.GotHornet == false)
        {
            P("Euh hello? May I go in please?");
            N("Name?");
            P("SKIBEE");
            S("No");
            P("No? No what?");
            N("No access.");
            P("So, how can I get access?");
            S("You can’t.");
            N("Get lost.");


            A("Give up");
            A("Warn about the hornet");

            AnswerID = 1;
            if (ifX(AnswerID) == 1)
            {
                End();
            }
            if (ifX(AnswerID) == 2)
            {
                P("Guards! Guards!");
                N("What?");
                P("The crack! There is a hornet coming through the crack!");
                S("Liar");

                // Sounds of commotion

                N("Hurry!");
                P("Bee safe!");
                End();
            }
        }
        if (InfoStorage.Instance.GotHornet == true)
        {
            P("This antenna should be enough proof. I hope the queen will believe me.");
            N("Beware.");
            P("Beware of what?");
            S("Secreterbee.");
            P("Why, what is wrong with her?");
            N("She’s beetchy.");
            P("How do I get past her?");
            S("Compliment.");
            N("Not too much.");
            P("Okayy…. Thank you!");
            End();

        }
    }
    void QueenScreech()
    {
        InfoStorage.Instance.PlayMusic("Queen");
    }
    void SleepingDialogue()
    {

    }
#endregion
}