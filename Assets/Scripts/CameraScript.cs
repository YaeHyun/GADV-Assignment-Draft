using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target; // The player sprite gameobject
    public float smoothTime = 0.3f; // The time for the camera to catch up to the player
    public Vector3 offset = new Vector3(0, 0, -10); // The offset of the camera to the player

    private Vector3 currentVelocity = Vector3.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void LateUpdate()
    {
        Vector3 targetPosition = target.position + offset; // Camera position is set to the target which is the player sprite and the offset value is added to offset the camera
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime); // Dampens the camera to follow the player sprite
    }
}
