using UnityEngine;

public interface IRigidEntity{
    bool IsGrounded {get;set;}
    Rigidbody2D Rigidbody {get;set;}
    Collider2D Collider {get;set;}
    SpriteRenderer SpriteRenderer {get;set;}
    Vector2 GetPosition();
}
