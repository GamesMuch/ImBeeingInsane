using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    RaycastHit hit;
    Ray mouseRay;

    bool inTrigger;

    public SceneAsset scene;

    public int doorID;


    void Update()
    {
        if (inTrigger && Input.GetKeyDown(KeyCode.Mouse0))
        {
            MouseCheck();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        inTrigger = true;
    }
    private void OnTriggerExit(Collider other)
    {
        inTrigger = false;
    }


    void MouseCheck() {
        mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseRay, out hit))
        {
            if (hit.collider.name == gameObject.name)
            {
                
                InfoStorage.Instance.DoorID = doorID;
                print(InfoStorage.Instance.DoorID);
                SceneManager.LoadScene(scene.name);

            }
        }
    }
}
