using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    public struct talkAvatar
    {
        public Image MouthOpen;
        public Image MouthClosed;
    }

    public InfoStorage Info;

    public talkAvatar NPCAvatar;

    public Image DialogueImage;
    public enum NPC { none, worker, grandpa, queen, test }
    public NPC ThisNPC = NPC.none;

    string john = "Freddie Mercury Johnson The Third";

    public List<(int, string)> D = new List<(int, string)>();

    public int lettersPerSecond = 3;

    public List<string> partsOfLines = new();

    float time;

    public TextMeshProUGUI TextBox = null;

    public bool NextLine = true;
    public bool Tapped;

    int line;

    #region SourceCode
    void Start()
    {
        TestDialogue();
        OnTalk();
    }
    public void OnTalk()
    {
        line = 0;
    }
    void TryTalk()
    {
        if (Tapped && NextLine)
        {
            line++;
            Tapped = false;
            NextLine = false;
            int nr = D[line].Item1;
            string nextLine = D[line].Item2;
            print(nextLine);
            Talk(nr, nextLine);

        }
    }
    void Talk(int nr, string sentence)
    {
        partsOfLines.Clear();
        TextBox.text = "";
        for (int i = 0; i < sentence.Length; i += lettersPerSecond)
        {

            int size = Mathf.Min(lettersPerSecond, sentence.Length - i);
            partsOfLines.Add(sentence.Substring(i, size));

            NextLine = true;
        }
        StartCoroutine(AddToText(nr));

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Tapped = true;
            TryTalk();
        }
        else
        {
            Tapped = false;
        }
    }

    void P(string text) => D.Add((0, text));
    void N(string text) => D.Add((1, text));

    IEnumerator AddToText(int nr)
    {
        foreach (string s in partsOfLines) {
            TextBox.text += s;

            StartCoroutine(TalkAvatar(nr));
            yield return (1000 / lettersPerSecond);
        }
    }
    IEnumerator TalkAvatar(int nr)
    {
        if (nr == 0) {
            DialogueImage = Info.playerAvatar.MouthOpen;
            yield return (1000/lettersPerSecond/2);
            DialogueImage = Info.playerAvatar.MouthClose;
            yield return (1000 / lettersPerSecond);
        }
        if (nr == 1)
        {
            DialogueImage = NPCAvatar.MouthOpen;
            yield return (1000 / lettersPerSecond / 2);
            DialogueImage = NPCAvatar.MouthClosed;
            yield return (1000 / lettersPerSecond);
        }
        
    }
    #endregion

    void InitializeDialogue() { }
    //etc
    void TestDialogue()
    {
        N("Hey");

        P("Who are you?");

        N("I am the guide");

        P("What do you guide?");

        N("I guide people to the promised land");

        P("No thanks, i dont like America");
    }
}