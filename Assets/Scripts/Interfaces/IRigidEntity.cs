using UnityEngine;

public interface IRigidEntity{
    public bool IsGrounded();
    public Rigidbody2D GetRigidbody2D();
    public Collider2D GetCollider2D();
    public SpriteRenderer GetSpriteRenderer();
    public Vector2 GetPosition();
}
