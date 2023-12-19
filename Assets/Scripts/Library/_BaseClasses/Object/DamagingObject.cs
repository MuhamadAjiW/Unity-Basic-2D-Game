using System;
using UnityEngine;

public class DamagingObject : MonoBehaviour, IDamagingEntity{
    [SerializeField] protected float baseDamage = 10f;
    private float damage;
    public float Damage {
        get => damage;
        set => damage = value;
    }
    public event Action OnDamage;
    protected void Awake(){
        Damage = baseDamage;
    }
    protected void InvokeOnDamage(){
        OnDamage?.Invoke();
    }
}