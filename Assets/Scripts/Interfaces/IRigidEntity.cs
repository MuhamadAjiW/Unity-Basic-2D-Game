using UnityEngine;

public interface IRigidEntity{
    public bool isGrounded();
    public Rigidbody2D GetRigidbody2D();
    public Collider2D GetCollider2D();
    public SpriteRenderer GetSpriteRenderer();
}
