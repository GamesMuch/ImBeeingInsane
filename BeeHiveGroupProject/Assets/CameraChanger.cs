using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    public Vector3 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            print(collision.gameObject.transform.position);
            Vector3 pos = collision.gameObject.transform.position;

            InfoStorage.Instance.cam.transform.position = pos + offset;
        }
    }
}
