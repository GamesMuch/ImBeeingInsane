using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    public int doorID;
    public GameObject doorSpawn;

    private void OnCollisionEnter(Collision other)
    {
        InfoStorage.Instance.currentDoor = gameObject;
        InfoStorage.Instance.DoorID = doorID;
        InfoStorage.Instance.OtherDoor();
    }
}
