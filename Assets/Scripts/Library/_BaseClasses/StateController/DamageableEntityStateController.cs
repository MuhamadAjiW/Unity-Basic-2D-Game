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

    public bool Damaged => damaged;

    protected void invokeDamageDelayOver(){
        OnDamageDelayOver?.Invoke();
    }

    protected abstract void OnDamaged();
    protected abstract void OnDeath();
}
