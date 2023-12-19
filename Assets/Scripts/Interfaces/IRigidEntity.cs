using UnityEngine;

public interface IRigidEntity{
    bool Grounded {get;}
    Rigidbody2D Rigidbody {get;}
    Collider2D Collider {get;}
    SpriteRenderer SpriteRenderer {get;}
    Vector2 Position {get;}
}
