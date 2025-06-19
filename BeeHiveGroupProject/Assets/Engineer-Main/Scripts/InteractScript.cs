using UnityEngine;
using UnityEngine.Events;

public class InteractScript : MonoBehaviour
{

    
    public UnityEvent OnClick;
    public GameObject pressButton;
    public PlayerMovement Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

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

    public void Interaction()
    {
        Player.canMove = false;
        OnClick.Invoke();
        //Debug.Log(ChooseUI);
    }
}
