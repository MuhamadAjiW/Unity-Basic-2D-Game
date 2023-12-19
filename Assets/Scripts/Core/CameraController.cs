using Unity.VisualScripting;
using UnityEngine;

public class CameraController {
    private Camera activeCamera;

    public CameraController(Camera camera){
        activeCamera = camera;
        activeCamera.enabled = true;
    }

    public void SwapCamera(Camera camera){
        activeCamera.enabled = false;
        activeCamera = camera;
        activeCamera.enabled = true;
    }

    public void CameraEnabled(bool enabled){
        activeCamera.enabled = true;
    }

    public void ResetCameraBehaviour(){
        GameObject.Destroy(activeCamera.GetComponent<CameraBehaviour>());
    }

    public void setFollow(Transform objectToFollow){
        ResetCameraBehaviour();
        FollowObject follow = activeCamera.AddComponent<FollowObject>();
        follow.objectToFollow = objectToFollow;
    }
}