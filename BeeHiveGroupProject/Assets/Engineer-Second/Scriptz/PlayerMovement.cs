using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public NavMeshAgent agent;

    public bool canMove = true;
    bool isMoving;

    float CheckCooldown = 0.4f;
    float currentTime;

    Vector3 pastLocation = Vector3.zero;

    public List<GameObject> openUis = new List<GameObject>();
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (canMove == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                agent.isStopped = false;
                currentTime = 0;
                isMoving = true;
                MoveToLocation();
                Debug.Log("start moving");
            }
        }
        if (isMoving == true)
        {
            currentTime += Time.deltaTime;
            if (CheckCooldown < currentTime)
            {
                currentTime = 0;
                if (Vector3.Distance(transform.position, pastLocation) <= 0.1f)
                {
                    agent.isStopped = true;
                }
                else
                {
                    pastLocation = transform.position;
                }
            }
        }
    }

    void MoveToLocation()
    {
        Ray raycast = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(raycast, out RaycastHit hit, 80);

        if (canMove == true)
        {
            if (hit.collider.CompareTag("Floor"))
            {
                agent.destination = hit.point;
            }
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Room Collider"))
    //    {
    //        roomLocation = other.transform.position;
    //    }
    //}

}