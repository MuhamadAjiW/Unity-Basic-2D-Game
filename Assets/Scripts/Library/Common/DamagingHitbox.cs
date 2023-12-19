using System;
using UnityEngine;

public class DamagingHitbox : MonoBehaviour, IDamagingEntity{
    [SerializeField] private float damage = 50f;
    public float Damage {
        get {return damage;}
        set {damage = value;}
    }

    public event Action OnDamage;

    void OnTriggerEnter2D(Collider2D otherCollider){
        Debug.Log(string.Format("Collision in hitbox of {0} by {1}", transform.parent.name, otherCollider.transform.name));
        if(!otherCollider.transform.TryGetComponent<IDamageableEntity>(out var enemyScript)) return;
        enemyScript.InflictDamage(Damage);
    }   
}