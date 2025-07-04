using System.Collections;
using UnityEngine;

public class InteractScript : MonoBehaviour
{




    public PlayerMovement Player;
    public GameObject parent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        parent = transform.parent.gameObject;
    }
    public IEnumerator Interaction()
    {
        if (transform.parent.GetComponentInChildren<DialogueBox>().inDialogue == false)
        {

            while (Vector3.Distance(Player.transform.position, gameObject.transform.position) > 3)
            {

                yield return new WaitForSeconds(0.1f);
            }



            GameObject.Find("InitializerGameManager").GetComponent<StartGame>().DialogueBox.SetActive(true);
            GameObject par = transform.parent.gameObject;
            par.GetComponentInChildren<DialogueBox>().OnTalk();
        }
        else
        {
            StopCoroutine(Interaction());
        }
        //Debug.Log(ChooseUI);
    }
}
