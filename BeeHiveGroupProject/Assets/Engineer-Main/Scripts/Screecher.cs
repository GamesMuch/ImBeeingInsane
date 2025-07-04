using UnityEngine;

public class Screecher : MonoBehaviour
{
    GameObject otherObj;

    public void Screeches()
    {
        if (otherObj != null)
        {
            otherObj.GetComponentInChildren<DialogueBox>().IsScreeched = true;
            InfoStorage.Instance.PlayMusic("Screech");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            otherObj = other.gameObject;
           
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            otherObj = null;
           
        }
    }
}
