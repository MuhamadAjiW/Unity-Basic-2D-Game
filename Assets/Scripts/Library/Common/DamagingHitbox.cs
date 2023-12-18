using System;
using UnityEngine;

public class DamagingHitbox : MonoBehaviour, IDamagingEntity{
    [SerializeField] float damage = 50f;

    public event Action OnDamage;

    public float GetDamage(){
        return damage;
    }

    void OnTriggerEnter2D(Collider2D otherCollider){
        Debug.Log(string.Format("Collision in hitbox of {0} by {1}", transform.parent.name, otherCollider.transform.name));
        if(!otherCollider.transform.TryGetComponent<IDamageableEntity>(out var enemyScript)) return;
        enemyScript.InflictDamage(GetDamage());
    }

    
}