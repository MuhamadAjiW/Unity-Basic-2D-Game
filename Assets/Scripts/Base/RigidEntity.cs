using UnityEngine;

public class RigidEntity : MonoBehaviour{
    public Rigidbody2D rigidBody;
    new public Collider2D collider;
    public SpriteRenderer sprite;

    public bool isGrounded = false;

    public RigidEntity(bool grounded){
        this.isGrounded = grounded;
    }

    public void Awake(){
        this.rigidBody = GetComponent<Rigidbody2D>();
        this.sprite = GetComponent<SpriteRenderer>();
        this.collider = GetComponent<Collider2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag(Constants.GROUND_TAG) && !isGrounded){
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision){
        if (collision.gameObject.CompareTag(Constants.GROUND_TAG) && isGrounded){
            isGrounded = false;
        }
    }
}