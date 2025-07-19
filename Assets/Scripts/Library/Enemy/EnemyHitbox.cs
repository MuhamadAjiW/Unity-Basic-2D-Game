using UnityEngine;
using static ConfigurationManager;

public class EnemyHitbox : DamagingHitbox
{
    protected void Awake()
    {
        Damage *= ConfigurationManager.Instance.enemyConfig.GLOBAL_DAMAGE_MULTIPLIER;
    }
}