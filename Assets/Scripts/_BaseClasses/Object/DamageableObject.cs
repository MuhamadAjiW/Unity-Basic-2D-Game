using System;
using UnityEngine;

public class DamageableObject : RigidObject, IDamageableEntity
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float health = 100;

    public float MaxHealth {
        get => maxHealth;
        set => maxHealth = value > 0? value : 0;
    }

    public float Health {
        get => health;
        set => health = value > 0? (value > MaxHealth? MaxHealth : value) : 0;
    }
    public bool Dead => health <= 0;
    public event Action OnDeath;
    public event Action OnDamaged;
    public event Action OnHeal;

    protected new void Awake(){
        base.Awake();
    }
    protected void InvokeOnDeath(){
        OnDeath?.Invoke();
    }
    protected void InvokeOnDamaged(){
        OnDamaged?.Invoke();
    }
    protected void InvokeOnHeal(){
        OnHeal?.Invoke();
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

    public virtual float InflictHeal(float heal){
        Health += heal;
        InvokeOnHeal();
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