using System;
using UnityEngine;

public class DamagingHitbox : DamagingObject{
    void OnTriggerStay2D(Collider2D otherCollider){
        Debug.Log(string.Format("Collision in hitbox of {0} by {1}", transform.name, otherCollider.transform.name));
        if(!otherCollider.transform.TryGetComponent<IDamageableEntity>(out var otherScript)) return;
        otherScript.InflictDamage(Damage);
        InvokeOnDamage();
    }
}