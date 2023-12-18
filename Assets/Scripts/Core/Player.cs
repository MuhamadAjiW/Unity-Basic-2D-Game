using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : RigidEntity, MovingEntity
{
    private float currentForce = 0;
    public float walkForce = 100;
    public float walkSpeed = 25;
    public float sprintForce = 150;
    public float sprintSpeed = 50;
    public float jumpForce = 600;
    public int jumpCounter = 2;


    private PlayerMovement movement;
    private PlayerStateController stateController;

    public Player(bool grounded) : base(grounded){}

    public PlayerState getPlayerState(){
        return this.stateController.getState();
    }

    private new void Awake(){
        base.Awake();
        this.movement = new PlayerMovement(this);
        this.stateController = new PlayerStateController(this);
    }

    public float getHorizontalForce(){
        if(isGrounded){
            switch (stateController.getState()){
                case PlayerState.WALKING:
                    currentForce = this.walkForce;
                    break;
                case PlayerState.SPRINTING:
                    currentForce = this.sprintForce;
                    break;
                default:
                    currentForce = 0;
                    break;
            }
        } else{
            currentForce =
                     stateController.getState() == PlayerState.STANCE? 0 :
                     currentForce > Constants.PLAYER_JUMP_MINIMUM_SPEED?
                     currentForce : Constants.PLAYER_JUMP_MINIMUM_SPEED;
        }

        return currentForce;
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
