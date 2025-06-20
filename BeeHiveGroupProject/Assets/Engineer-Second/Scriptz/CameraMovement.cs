using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class CameraMovement : MonoBehaviour
{
    public PlayerMovement player;
    public Vector3 cameraOffset = new Vector3(12,12,0);
    public float cameraSpeed = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        if (transform.position != player.roomLocation + cameraOffset)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.roomLocation + cameraOffset, cameraSpeed * Time.deltaTime);
        }
    }
}
