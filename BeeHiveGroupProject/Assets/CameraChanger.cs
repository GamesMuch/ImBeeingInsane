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
            pos.y = InfoStorage.Instance.cam.transform.position.y;

            InfoStorage.Instance.cam.transform.position = pos + offset;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {

            Vector3 pos = other.gameObject.transform.position;
            pos.y = InfoStorage.Instance.cam.transform.position.y;
            InfoStorage.Instance.cam.transform.position = pos + offset;
        }
    }
}
