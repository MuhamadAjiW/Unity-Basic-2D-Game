using System;
using UnityEngine;

public class DamageableObject : RigidObject, IDamageableEntity
{
    [SerializeField] private float health = 100;

    public float Health {
        get => health;
        set => health = value > 0? value : 0;
    }
    public bool Dead => health <= 0;
    public event Action OnDeath;
    public event Action OnDamaged;

    protected new void Awake(){
        base.Awake();
    }
    protected void InvokeOnDeath(){
        OnDeath?.Invoke();
    }
    protected void InvokeOnDamaged(){
        OnDamaged?.Invoke();
    }

    // Overrideables
    public virtual bool Damageable => !Dead;
    public virtual float InflictDamage(float damage){
        Health -= damage;
        InvokeOnDamaged();
        if(Dead) InvokeOnDeath();
        Debug.Log(string.Format("{0} remaining health: {1}", name, Health));

        return Health;
    }

    protected void Update(){
        if(GameController.instance.IsPaused()) return;
    }

    protected void FixedUpdate(){
        if(GameController.instance.IsPaused()) return;
    }
}