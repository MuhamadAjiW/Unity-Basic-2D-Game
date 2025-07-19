using UnityEngine;

public class WeaponObject : DamagingObject{
    private SpriteRenderer sprite;
    public Animator animator; 
    [SerializeField] private float staminaCost;
    public SpriteRenderer SpriteRenderer => sprite;
    public Animator Animator => animator;
    public float StaminaCost{
        get { return staminaCost; }
        set { staminaCost = value > 0 ? value : staminaCost; }
    }

    // Sprite should always be inside a child
    protected void Awake(){
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Overrideables
    public virtual void Hit(Collider2D otherCollider){
        Debug.Log(string.Format("Collision in hitbox of {0} by {1}", transform.name, otherCollider.transform.name));
        if(otherCollider.transform.TryGetComponent<IDamageableEntity>(out var damageableEntity))

        Debug.Log(damageableEntity);
        if(damageableEntity.Damageable){
            Debug.Log("Entity damageable");

            damageableEntity.InflictDamage(Damage);
            InvokeOnDamage();

            if(otherCollider.transform.TryGetComponent<RigidObject>(out var rigidObject)) Knock(rigidObject.Rigidbody);
        }
    }

    public virtual void Attack(){}
}