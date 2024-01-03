using System;
using UnityEngine;

public abstract class EnemyObject : DamageableObject, IDamageableEntity{
    protected new void Awake(){
        base.Awake();
        MaxHealth *= EnemyConfig.GLOBAL_HEALTH_MULTIPLIER;
        Health *= EnemyConfig.GLOBAL_HEALTH_MULTIPLIER;
    }
}