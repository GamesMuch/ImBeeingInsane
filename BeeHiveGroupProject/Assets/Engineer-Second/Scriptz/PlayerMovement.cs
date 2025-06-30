using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Vector3 roomLocation;
    public bool canMove = true;

    public bool ImportantMovement = false;
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
                // Prevent movement if pointer is over UI
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;  // Ignore clicks on UI buttons
                }

                agent.isStopped = false;
                currentTime = 0;
                isMoving = true;
                MoveToLocation();
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

        if (ImportantMovement == false)
        {

            if (canMove == true)
            {
                if (hit.collider.CompareTag("Floor"))
                {
                    agent.destination = hit.point;
                }
                if (hit.collider.CompareTag("NPC"))
                {
                    agent.destination = hit.point;
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
        else if (ImportantMovement == true)
        {
            if (canMove == true)
            {
                
                if (hit.collider.CompareTag("NPC"))
                {
                    Debug.Log("Hit the npc");
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