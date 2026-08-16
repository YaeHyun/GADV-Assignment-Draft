using UnityEngine;

public class ParallaxScript : MonoBehaviour
{

    public Transform player;
    public float parallaxMultiplier = 0.5f;

    private Vector3 previousPlayerPosition;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        previousPlayerPosition = player.position;
    }

    void LateUpdate()
    {
        Vector3 deltaMovement =
            player.position - previousPlayerPosition;

        transform.position += new Vector3(
            deltaMovement.x * parallaxMultiplier,
            0,
            0);

        previousPlayerPosition = player.position;
    }
}
