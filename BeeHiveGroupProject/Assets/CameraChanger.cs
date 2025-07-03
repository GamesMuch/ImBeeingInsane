using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    public Vector3 offset;
    bool hasAdjustedCamera = false;

    void OnCollisionEnter(Collision collision)
    {
        print("Collided: "+ collision.collider.tag);
        if (!hasAdjustedCamera && collision.gameObject.CompareTag("Floor"))
        {
            print("augh");
            hasAdjustedCamera = true;

            Vector3 pos = collision.gameObject.transform.position;
            pos.y = InfoStorage.Instance.cam.transform.position.y;

            InfoStorage.Instance.cam.transform.position = pos + offset;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        hasAdjustedCamera = false;
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Floor"))
    //    {

    //        Vector3 pos = other.gameObject.transform.position;
    //        pos.y = InfoStorage.Instance.cam.transform.position.y;
    //        InfoStorage.Instance.cam.transform.position = pos + offset;
    //    }
    //}
}
