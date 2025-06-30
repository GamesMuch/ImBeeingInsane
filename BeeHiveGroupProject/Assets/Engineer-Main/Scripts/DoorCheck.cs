using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorCheck : MonoBehaviour
{
    

    void Start()
    {
        CheckIfExists();
    }
    void CheckIfExists()
    {
        if (InfoStorage.Instance != null && InfoStorage.Instance.player != null && InfoStorage.Instance.DoorID != 0)
        {
            bool found = false;
            foreach (DoorScript d in InfoStorage.Instance.stairs)
            {
                print(d.gameObject.name);
                Debug.Log("Wagh");
                if (d.doorID == InfoStorage.Instance.DoorID)
                {
                    InfoStorage.Instance.player.transform.position = d.transform.position;
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                if (InfoStorage.Instance.nullSpawn != null)
                {
                    InfoStorage.Instance.player.transform.position = InfoStorage.Instance.nullSpawn.position;
                }
                Debug.LogWarning("There is no spawn door");
            }
        }
    }
}
