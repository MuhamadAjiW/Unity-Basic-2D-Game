using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : RigidEntity, MovingEntity
{
    public float walkForce = 5;
    public float sprintForce = 20;
    public float jumpForce = 10000;
    public int jumpCounter = 2;

    private PlayerMovement movement;
    private PlayerStateController stateController;

    public Player(bool grounded) : base(grounded){}

    private new void Awake(){
        base.Awake();
        this.movement = new PlayerMovement(this);
        this.stateController = new PlayerStateController(this);
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

    void Update(){
        movement.jump();
    }

    void FixedUpdate(){
        // Util.printPlayerState(this.stateController.GetState());
        this.stateController.updateState();
        movement.move();
    }
}
