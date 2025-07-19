using UnityEngine;

public class ConfigurationManager : MonoBehaviour
{
    public static ConfigurationManager Instance { get; private set; }

    [Header("Game Configurations")]
    public ControlsConfig controlsConfig;
    public CameraConfig cameraConfig;
    public EnemyConfig enemyConfig;
    public PlayerConfig playerConfig;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Duplicate ConfigurationManager detected. Destroying this one.");
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("ConfigurationManager initialized.");
        }
    }
}