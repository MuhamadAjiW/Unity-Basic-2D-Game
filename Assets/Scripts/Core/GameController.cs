using System;
using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController instance;
    private static CameraController mainCamera;
    public static CameraController MainCamera { get => mainCamera; set => mainCamera = value; }

    private bool paused = false;

    event Action OnInteract;
    event Action OnPause;
    event Action OnUnpause;
    event Action OnPlayerHit;
    event Action OnPlayerStaminaUse;

    private int[] EventStack;

    private void Awake(){
        if(instance == null) instance = this;
        MainCamera = new CameraController(this.GetComponentInChildren<Camera>());
    }
    
    public bool IsPaused(){
        return paused;
    }

    public void Pause(){
        Time.timeScale = 0;
        paused = true;
        OnPause?.Invoke();
    }

    public void Unpause(){
        Time.timeScale = 1;
        paused = false;
        OnUnpause?.Invoke();
    }

    void Update(){
         if(Input.GetKeyDown(KeyCode.Escape)){
            if (paused){
                Unpause();
            }
            else{
                Pause();
            }
         }
    }
}