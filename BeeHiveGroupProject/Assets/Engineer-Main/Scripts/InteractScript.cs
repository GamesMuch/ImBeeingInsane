using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class InteractScript : MonoBehaviour
{

    
    public UnityEvent OnClick;
    public GameObject pressButton;
    public PlayerMovement Player;
    GameObject gameObj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {
        gameObj = transform.parent.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pressButton.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pressButton.SetActive(false);
            

        }
    }

    public IEnumerator Interaction()
    {
        if (gameObj.GetComponentInChildren<DialogueBox>().inDialogue == false)
        {
            Debug.Log("WeWait");
            while (Vector3.Distance(Player.transform.position, gameObject.transform.position) > 2)
            {
               
                yield return new WaitForSeconds(0.1f);
            }


            Debug.LogWarning("Invoking event");
            OnClick.Invoke();
        }
        else
        {
            StopCoroutine(Interaction());
        }
        //Debug.Log(ChooseUI);
    }
}
