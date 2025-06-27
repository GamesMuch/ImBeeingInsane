using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class LocalStorage : MonoBehaviour
{
    public GameObject player;

    public Transform nullSpawn;

    public GameObject blinkItem;

    public List<DoorScript> stairs = new();

    public List<DialogueBox> dialogueBoxes = new();

    int answerNr;

    public bool InDialogue;

    void Start()
    {
        blinkItem.SetActive(true);
        blinkItem.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {

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
