using System.Collections.Generic;
using UnityEngine;

public class PlantLevels : MonoBehaviour
{
    public List<GameObject> flowerStates = new List<GameObject> ();
    int flowerCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextState()
    {
        if (flowerCount < flowerStates.Count -1)
        {
            flowerStates[flowerCount].SetActive(false);
            flowerCount++;
            flowerStates[flowerCount].SetActive(true);
        }
        else
        {
            Debug.Log("Ran out");
        }
    }
}
