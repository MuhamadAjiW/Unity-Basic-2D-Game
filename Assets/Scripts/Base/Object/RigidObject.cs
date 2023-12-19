using UnityEngine;

public class RigidObject : MonoBehaviour, IRigidEntity{
    private Rigidbody2D rigidBody;
    new private Collider2D collider;
    private SpriteRenderer sprite;

    private bool grounded = false;

    public bool IsGrounded{
        get { return grounded;}
        set { grounded = value; }
    }
    public Rigidbody2D Rigidbody {
        get { return rigidBody; }
        set { rigidBody = value; }
    }
    public Collider2D Collider {
        get { return collider; }
        set { collider = value; }
    }
    public SpriteRenderer SpriteRenderer {
        get { return sprite; }
        set { sprite = value; }
    }

    public Rigidbody2D GetRigidbody2D(){
        return rigidBody;
    }

    public Collider2D GetCollider2D(){
        return collider;
    }

    public SpriteRenderer GetSpriteRenderer(){
        return sprite;
    }
    
    public Vector2 GetPosition(){
        return transform.position;
    }

    public void Awake(){
        rigidBody = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag(EnvironmentConfig.GROUND_TAG) && !grounded){
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision){
        if (collision.gameObject.CompareTag(EnvironmentConfig.GROUND_TAG) && grounded){
            grounded = false;
        }
    }
}