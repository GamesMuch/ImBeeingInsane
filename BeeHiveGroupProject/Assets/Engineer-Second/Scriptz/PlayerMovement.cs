using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    NavMeshAgent agent;
    public Vector3 roomLocation;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToLocation();
        }

    }

    void MoveToLocation()
    {
        Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(raycast, out RaycastHit hit);
        if (hit.collider.CompareTag("Floor"))
        {
            agent.destination = hit.point;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Room Collider"))
        {
            roomLocation = other.transform.position;
        }
    }

}