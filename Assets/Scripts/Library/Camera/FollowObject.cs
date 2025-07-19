using UnityEngine;
using static ConfigurationManager;

public class FollowObject : CameraBehaviour
{
    public Transform objectToFollow;
    public float followingTime;
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        if (ConfigurationManager.Instance != null && ConfigurationManager.Instance.cameraConfig != null)
        {
            followingTime = ConfigurationManager.Instance.cameraConfig.FOLLOWING_SPEED;
        }
        else
        {
            Debug.LogWarning("CameraConfig not loaded in ConfigurationManager for FollowObject. Using default value.");
            followingTime = 0.2f; // Default value
        }
    }

    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(objectToFollow.position.x, objectToFollow.position.y, transform.position.z);
        Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, followingTime);
        transform.position = newPosition;
    }
}