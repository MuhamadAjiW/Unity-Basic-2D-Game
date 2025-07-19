using System;
using UnityEngine;

public class DamagingObject : MonoBehaviour, IDamagingEntity{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float knockbackPower = 10f;
    [SerializeField] private Direction knockbackDirection = Direction.ALL;
    public float Damage {
        get => damage;
        set => damage = value;
    }
    public float KnockbackPower{
        get => knockbackPower;
        set => knockbackPower = value;
    }
    public Direction KnockbackDirection{
        get => knockbackDirection;
        set => knockbackDirection = value;
    }


    public event Action OnDamage;
    protected void InvokeOnDamage(){
        OnDamage?.Invoke();
    }

    protected void Knock(Rigidbody2D rigidbody, float knockbackResistance = 0){
        Vector2 knockback = new Vector2(0,0);
        float knockbackModifier;
        switch (knockbackResistance){
            case( <= 0):
                knockbackModifier = knockbackPower;
                break;
            case( > 65535):
                knockbackModifier = 0;
                break;            
            default:
                knockbackModifier = knockbackPower / knockbackResistance;
                break;
        }
        
        if(knockbackDirection == Direction.ALL){
            Vector2 direction = (Vector2) transform.position - rigidbody.position;
            float angle = Mathf.Atan2(direction.y, direction.x);
            knockback =  new Vector2(-Mathf.Cos(angle), -Mathf.Sin(angle)) * knockbackModifier;
        }
        else{
            if((knockbackDirection & Direction.RIGHT) != 0){
                knockback.x += knockbackModifier;
            } else if((knockbackDirection & Direction.LEFT) != 0){
                knockback.x -= knockbackModifier;
            }

            if((knockbackDirection & Direction.UP) != 0){
                knockback.y += knockbackModifier;
            } else if((knockbackDirection & Direction.DOWN) != 0){
                knockback.y -= knockbackModifier;
            }
        }

        // Debug.Log(string.Format("Knocking back with power of {0}", knockback));
        rigidbody.AddForce(knockback, ForceMode2D.Impulse);
    }
}