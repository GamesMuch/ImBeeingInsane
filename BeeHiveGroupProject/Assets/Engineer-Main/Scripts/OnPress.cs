using UnityEngine;
using UnityEngine.InputSystem;

public class OnPress : MonoBehaviour
{
    public InteractScript parentInteraction;

    RaycastHit hit;
    Ray mouseRay;
    Mouse playerMouse;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MousePress();
        }
        
    }
    void MousePress()
    {
        
        mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseRay, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                parentInteraction.StartCoroutine(parentInteraction.Interaction());
            }
        }
    }
}
