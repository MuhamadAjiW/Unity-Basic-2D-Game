using System;
using UnityEngine;
 
public class GameController : MonoBehaviour
{
    public static GameController instance;
    private static CameraController mainCamera;
    public static ControlsConfig Controls { get; private set; } // Static access to ControlsConfig
    public static CameraController MainCamera { get => mainCamera; set => mainCamera = value; }

    private bool paused = false;

    event Action OnInteract;
    event Action OnPause;
    event Action OnUnpause;
    event Action OnCutsceneStart;
    event Action OnCutsceneEnd;

    private int[] EventStack;

    private void Awake()
    {
        if (instance == null) instance = this;
        MainCamera = new CameraController(this.GetComponentInChildren<Camera>());
        Controls = Resources.Load<ControlsConfig>("ControlsConfig"); // Load from Resources folder
        if (Controls == null)
        {
            Debug.LogError("ControlsConfig not found in Resources folder. Please create one at Assets/Resources/ControlsConfig.asset");
        }
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
            if (Controls != null && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(Controls.Confirm)))
            {
                CutsceneManager.Instance.NextDialogue();
            }
            return;
        }
 
        if (Controls != null && Input.GetKeyDown(Controls.Cancel))
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