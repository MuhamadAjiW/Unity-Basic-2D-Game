using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, MovingEntity
{
    public float walkForce = 5;
    public float sprintForce = 20;
    public float jumpForce = 5;

    private PlayerMovement movement;
    private PlayerStateController stateController;
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;

    private void Awake(){
        this.movement = new PlayerMovement(this);
        this.stateController = new PlayerStateController(this);
        this.rigidbody2D = GetComponent<Rigidbody2D>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public float getHorizontalForce(){
        switch (stateController.GetState()){
            case PlayerState.WALKING:
                return this.walkForce;
            case PlayerState.SPRINTING:
                return this.sprintForce;
            case PlayerState.JUMPING:
                return this.sprintForce;
            default: 
                return this.walkForce;
        }
    }

    public float getVerticalForce(){
        return this.jumpForce;
    }

    void FixedUpdate(){
        this.stateController.updateState();
        Util.printPlayerState(this.stateController.GetState());
        movement.move();
        movement.jump();
    }
}
