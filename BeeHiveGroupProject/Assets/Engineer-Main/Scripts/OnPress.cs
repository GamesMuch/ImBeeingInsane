using UnityEngine;
using UnityEngine.InputSystem;

public class OnPress : MonoBehaviour
{
    public InteractScript parentInteraction;

    RaycastHit hit;
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
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseRay, out hit))
        {
            print(hit.collider.name);
            if (hit.collider.name == gameObject.name)
            {
                parentInteraction.Interaction();
            }
        }
    }
}
