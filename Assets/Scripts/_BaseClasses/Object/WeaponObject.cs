using UnityEngine;

public class WeaponObject : DamagingObject{
    protected SpriteRenderer sprite;
    public Animator animator; 
    public SpriteRenderer SpriteRenderer => sprite;
    public Animator Animator => animator;

    // Sprite should always be inside a child
    protected void Awake(){
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public virtual void Attack(){}
}