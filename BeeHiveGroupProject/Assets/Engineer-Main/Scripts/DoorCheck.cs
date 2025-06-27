using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorCheck : MonoBehaviour
{
    public LocalStorage localStorage;

    void Start()
    {
        CheckIfExists();
    }
    void CheckIfExists()
    {
        if (InfoStorage.Instance != null && localStorage.player != null && InfoStorage.Instance.DoorID != 0)
        {
            bool found = false;
            foreach (DoorScript d in localStorage.stairs)
            {
                print(d.gameObject.name);
                Debug.Log("Wagh");
                if (d.doorID == InfoStorage.Instance.DoorID)
                {
                    localStorage.player.transform.position = d.transform.position;
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                if (localStorage.nullSpawn != null)
                {
                    localStorage.player.transform.position = localStorage.nullSpawn.position;
                }
                Debug.LogWarning("There is no spawn door");
            }
        }
    }
}
