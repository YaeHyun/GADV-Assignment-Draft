using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;       // Drag your player sprite here
    public float smoothTime = 0.3f; // Time taken for the camera to catch up
    public Vector3 offset = new Vector3(0, 0, -10); // Keeps the camera back on the Z axis  

    private Vector3 currentVelocity = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void LateUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }
}
