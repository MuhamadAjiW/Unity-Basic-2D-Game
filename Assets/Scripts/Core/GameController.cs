using System;
using UnityEngine;
using static ConfigurationManager;
 
public class GameController : MonoBehaviour
{
    public static GameController instance;
    private static CameraController mainCamera;
    public static CameraController MainCamera { get => mainCamera; set => mainCamera = value; }
    public static CameraConfig CameraConfig { get => Instance.cameraConfig; }
    public static EnemyConfig EnemyConfig { get => Instance.enemyConfig; }
    public static PlayerConfig PlayerConfig { get => Instance.playerConfig; }

    private bool paused = false;

    event Action OnInteract;
    event Action OnPause;
    event Action OnUnpause;
    event Action OnCutsceneStart;
    event Action OnCutsceneEnd;

    private int[] EventStack;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make GameController persist across scenes
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Destroy duplicate GameController
        }
        MainCamera = new CameraController(this.GetComponentInChildren<Camera>());
        // ControlsConfig is now managed by ConfigurationManager
    }

    public bool IsPaused()
    {
        return paused;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        paused = true;
        OnPause?.Invoke();
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        paused = false;
        OnUnpause?.Invoke();
    }

    void Update()
    {
        // Prevent input during cutscenes
        if (CutsceneManager.Instance != null && CutsceneManager.Instance.IsCutsceneActive())
        {
            if (ConfigurationManager.Instance != null && ConfigurationManager.Instance.controlsConfig != null && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(ConfigurationManager.Instance.controlsConfig.Confirm)))
            {
                CutsceneManager.Instance.NextDialogue();
            }
            return;
        }
 
        if (ConfigurationManager.Instance != null && ConfigurationManager.Instance.controlsConfig != null && Input.GetKeyDown(ConfigurationManager.Instance.controlsConfig.Cancel))
        {
            if (paused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }

    public void StartCutscene()
    {
        Pause();
        OnCutsceneStart?.Invoke();
    }

    public void EndCutscene()
    {
        Unpause();
        OnCutsceneEnd?.Invoke();
    }
}