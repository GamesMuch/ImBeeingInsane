using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class OpenUI : MonoBehaviour
{
    public List<GameObject> UiObjects = new List<GameObject>();
    List<string> UiNames = new List<string>();
    
    void Start()
    {
        //Converts the gameobjects to a list with strings
        foreach (GameObject obj in UiObjects)
        {
            UiNames.Add(obj.name);
        }
    }
    public void OpenUi(string name)
    {
        //Opens the ui with the same name
        if (UiNames.Contains(name)){
            int activateNumber = UiNames.IndexOf(name);
            UiObjects[activateNumber].SetActive(true);
        }
    }
}
