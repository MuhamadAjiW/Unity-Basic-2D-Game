using UnityEngine;

public class WeaponObject : DamagingObject{
    protected SpriteRenderer sprite;
    public SpriteRenderer SpriteRenderer => sprite;
    public float knockbackResistance = 0; 

    // Sprite should always be inside a child
    protected void Awake(){
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    protected void Attack(){

    }
}