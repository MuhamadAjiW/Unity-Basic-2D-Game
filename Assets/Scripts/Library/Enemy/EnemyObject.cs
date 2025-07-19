using System;
using UnityEngine;
using static ConfigurationManager;

public abstract class EnemyObject : DamageableObject, IDamageableEntity
{
    protected new void Awake()
    {
        base.Awake();
        MaxHealth *= ConfigurationManager.Instance.enemyConfig.GLOBAL_HEALTH_MULTIPLIER;
        Health *= ConfigurationManager.Instance.enemyConfig.GLOBAL_HEALTH_MULTIPLIER;
    }
}