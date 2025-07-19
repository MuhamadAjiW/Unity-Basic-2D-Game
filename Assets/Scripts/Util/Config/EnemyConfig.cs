using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "ScriptableObjects/Enemy Config", order = 1)]
public class EnemyConfig : ScriptableObject
{
    [Header("State")]
    [HideInInspector] public float DAMAGED_STATE_DURATION = 1;

    [Header("Config")]
    public float GLOBAL_DAMAGE_MULTIPLIER = 1;
    public float GLOBAL_HEALTH_MULTIPLIER = 1;
}