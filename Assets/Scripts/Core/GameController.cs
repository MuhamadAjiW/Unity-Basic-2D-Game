using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController instance;
    public static CameraController mainCamera;

    private void Awake(){
        if(instance == null) instance = this;
        mainCamera = new CameraController(this.GetComponentInChildren<Camera>());
    }
}