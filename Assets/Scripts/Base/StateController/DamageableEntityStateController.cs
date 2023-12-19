using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class DamageableEntityStateController<T> : EntityStateController where T : MonoBehaviour, IDamageableEntity {
    protected bool damaged = false;
    public event Action OnDamageDelayOver;

    public DamageableEntityStateController(T entity) : base(entity){
        this.entity = entity;
        entity.OnDamaged += OnDamaged;
        entity.OnDeath += OnDeath;
    }

    public bool IsDamaged(){
        return damaged;
    }

    public void insertOnDamageDelayOver(Action action ){
        OnDamageDelayOver += action;
    }

    protected void invokeDamageDelayOver(){
        OnDamageDelayOver?.Invoke();
    }

    public abstract void OnDamaged();
    public abstract void OnDeath();
}
