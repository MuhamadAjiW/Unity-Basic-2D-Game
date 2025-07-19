using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D otherCollider)
    {
        WeaponObject weapon = transform.parent.GetComponent<WeaponObject>();
        weapon.Hit(otherCollider);
    }
}