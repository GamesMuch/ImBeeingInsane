using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    public Vector3 offset;
    bool hasAdjustedCamera = false;

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (!hasAdjustedCamera && collision.gameObject.CompareTag("Floor"))
    //    {
    //        hasAdjustedCamera = true;

    //        Vector3 pos = collision.gameObject.transform.position;
    //        pos.y = InfoStorage.Instance.cam.transform.position.y;

    //        InfoStorage.Instance.cam.transform.position = pos + offset;
    //    }
    //}
    //private void OnCollisionExit(Collision collision)
    //{
    //    hasAdjustedCamera = false;
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (!hasAdjustedCamera && other.gameObject.CompareTag("Floor"))
        {
            hasAdjustedCamera = true;

            Vector3 pos = other.gameObject.transform.position;
            pos.y = InfoStorage.Instance.cam.transform.position.y;

            InfoStorage.Instance.cam.transform.position = pos + offset;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        hasAdjustedCamera = false;
    }
}
