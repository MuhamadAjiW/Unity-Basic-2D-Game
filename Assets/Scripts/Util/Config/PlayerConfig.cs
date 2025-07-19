using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObjects/Player Config", order = 1)]
public class PlayerConfig : ScriptableObject
{
    [Header("Movement")]
    [HideInInspector] public int JUMP_DELAY = 10;
    [HideInInspector] public int JUMP_MINIMUM_SPEED = 5;
    [HideInInspector] public float JUMP_LOW_MULTIPLIER = 2f;
    [HideInInspector] public float FALL_SPEED_MULTIPLIER = 2.5f;
    [HideInInspector] public float MOVEMENT_SMOOTHING = 0.1f;

    [Header("State")]
    [HideInInspector] public float DAMAGED_STATE_DURATION = 1;

    [Header("Dash")]
    [HideInInspector] public float DASH_TRAIL_DURATION = 0.2f;

    [Header("Config")]
    public float GLOBAL_DAMAGE_MULTIPLIER = 1;
    public float GLOBAL_HEALTH_MULTIPLIER = 1;
}