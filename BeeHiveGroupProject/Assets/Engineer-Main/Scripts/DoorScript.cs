using UnityEngine;

[DisallowMultipleComponent]
public class DoorScript : MonoBehaviour
{
    public string doorID;

    private void OnCollisionEnter(Collision other)
    {
        print("Collided with " + other.gameObject.name);
        InfoStorage.Instance.currentDoor = gameObject;
        InfoStorage.Instance.DoorID = doorID;
        InfoStorage.Instance.OtherDoor();
    }
    public void MovePlayerHere()
    {
        InfoStorage.Instance.player.GetComponent<PlayerMovement>().agent.Warp(GetComponentInChildren<ChildMarker>().gameObject.transform.position);
    }
}
