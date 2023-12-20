using UnityEngine;

public class RigidObject : MonoBehaviour{
    protected Rigidbody2D rigidBody;
    new protected Collider2D collider;
    protected SpriteRenderer sprite;
    private bool grounded = false;

    public bool Grounded => grounded;
    public Rigidbody2D Rigidbody => rigidBody;
    public Collider2D Collider => collider;
    public SpriteRenderer SpriteRenderer => sprite;
    public Vector2 Position => transform.position;
    public float knockbackResistance = 0; 

    // Sprite should always be inside a child
    protected void Awake(){
        rigidBody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
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

    protected void Refresh(){
        Rigidbody.AddForce(Vector2.zero);
    }

    protected void Smoothen(){
        Vector2 dampVelocity = Vector2.zero;
        Vector2 velocity = Rigidbody.velocity;
        velocity.x = 0;
        Rigidbody.velocity = Vector2.SmoothDamp(Rigidbody.velocity, velocity, ref dampVelocity, EnvironmentConfig.MOVEMENT_SMOOTHING);
    }
}