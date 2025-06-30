using UnityEngine;

public class Screecher : MonoBehaviour
{
    GameObject otherObj;

    public void Screeches()
    {
        if (otherObj != null)
        {
            otherObj.GetComponentInChildren<DialogueBox>().IsScreeched = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            otherObj = other.gameObject;
            print(other.name);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            otherObj = null;
            print(other.name);
        }
    }
}
