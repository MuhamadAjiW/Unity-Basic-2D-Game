using System;
using UnityEngine;

public class DamagingHitbox : DamagingObject{
    void OnTriggerStay2D(Collider2D otherCollider){
        Debug.Log(string.Format("Collision in hitbox of {0} by {1}", transform.name, otherCollider.transform.name));
        if(otherCollider.transform.TryGetComponent<IDamageableEntity>(out var damageableEntity))

        Debug.Log(damageableEntity);
        if(damageableEntity.Damageable){
            Debug.Log("Entity damageable");

            damageableEntity.InflictDamage(Damage);
            InvokeOnDamage();

            if(otherCollider.transform.TryGetComponent<RigidObject>(out var rigidObject)) Knock(rigidObject.Rigidbody);
        } else{
            Debug.Log("Entity not damageable");
        }
    }
}