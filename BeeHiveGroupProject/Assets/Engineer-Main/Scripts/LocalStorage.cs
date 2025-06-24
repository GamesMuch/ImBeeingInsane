using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class LocalStorage : MonoBehaviour
{
    public GameObject player;

    public Transform nullSpawn;

    public GameObject blinkItem;

    public List<DoorScript> doors = new();

    void Start()
    {
        blinkItem.SetActive(true);
        blinkItem.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
