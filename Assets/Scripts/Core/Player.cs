using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : RigidEntity, MovingEntity
{
    public float snapShotForce = 0;
    private float targetForce = 5;
    public float walkForce = 5;
    public float sprintForce = 20;
    public float jumpForce = 700;
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
        if(isGrounded){
            switch (stateController.GetState()){
                case PlayerState.WALKING:
                    targetForce = this.walkForce;
                    break;
                case PlayerState.SPRINTING:
                    targetForce = this.sprintForce;
                    break;
                default:
                    targetForce = 0;
                    break;
            }
        } else{
            targetForce = snapShotForce > Constants.PLAYER_JUMP_MINIMUM_SPEED? snapShotForce : Constants.PLAYER_JUMP_MINIMUM_SPEED;
        }

        snapShotForce = Mathf.Lerp(snapShotForce, targetForce, Time.fixedDeltaTime * 5.0f);

        return snapShotForce;
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
