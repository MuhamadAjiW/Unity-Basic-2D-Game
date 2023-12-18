using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement {
    private Player player;
    private int jumpCounter;
    private bool jumpDelayOver = true;
    
    public PlayerMovement(Player player){
        this.player = player;
        this.jumpCounter = player.jumpCounter;
    }

    private Vector2 getPlayerPosition(){
        return player.transform.position;
    }

    private void setPlayerPosition(Vector2 newPosition){
        player.transform.position = newPosition;
    }

    public void move(){
        float keyPress = Input.GetAxis("Horizontal");

        Vector2 force = new Vector2(0,0);
        float playerHorizontalForce = player.getHorizontalForce();
        Vector2 velocity = this.player.rigidBody.velocity;
        PlayerState playerState = player.getPlayerState();

        if(keyPress == 0 || playerState == PlayerState.STANCE){
            if(player.isGrounded){
                velocity.x = Mathf.Lerp(velocity.x, 0, 0.08f * Constants.PLAYER_MOVEMENT_SMOOTHING);
            } else{
                velocity.x = Mathf.Lerp(velocity.x, 0, 0.02f * Constants.PLAYER_MOVEMENT_SMOOTHING);
            }
            this.player.rigidBody.velocity = velocity;
        } else{
            force = new Vector2(keyPress * playerHorizontalForce, 0);

            switch (playerState)
            {
                case (PlayerState.WALKING):
                    if(keyPress > 0 && velocity.x > this.player.walkSpeed){
                        velocity.x = Mathf.Lerp(velocity.x, this.player.walkSpeed, Time.fixedDeltaTime * Constants.PLAYER_MOVEMENT_SMOOTHING);
                        this.player.rigidBody.velocity = velocity;
                        break;
                    }
                    else if(keyPress < 0 && velocity.x < (-1) * this.player.walkSpeed){
                        velocity.x = Mathf.Lerp(velocity.x, (-1) * this.player.walkSpeed, Time.fixedDeltaTime * Constants.PLAYER_MOVEMENT_SMOOTHING);
                        this.player.rigidBody.velocity = velocity;
                        break;
                    }
                    this.player.rigidBody.AddForce(force, ForceMode2D.Impulse);
                    break;
                case (PlayerState.SPRINTING):
                    if(keyPress > 0 && velocity.x > this.player.sprintSpeed){
                        velocity.x = Mathf.Lerp(velocity.x, this.player.sprintSpeed, Time.fixedDeltaTime * Constants.PLAYER_MOVEMENT_SMOOTHING);
                        this.player.rigidBody.velocity = velocity;
                        break;
                    }
                    else if(keyPress < 0 && velocity.x < (-1) * this.player.sprintSpeed){
                        velocity.x = Mathf.Lerp(velocity.x, (-1) * this.player.sprintSpeed, Time.fixedDeltaTime * Constants.PLAYER_MOVEMENT_SMOOTHING);
                        this.player.rigidBody.velocity = velocity;
                        break;
                    }
                    this.player.rigidBody.AddForce(force, ForceMode2D.Impulse);
                    break;
                case (PlayerState.JUMPING):
                case (PlayerState.FALLING):
                    if(keyPress > 0 && velocity.x > 0){
                        break;
                    }
                    else if(keyPress < 0 && velocity.x < 0){
                        break;
                    }
                    this.player.rigidBody.AddForce(force/2, ForceMode2D.Impulse);
                    break;
            }
        }

        if (player.rigidBody.velocity.y < 0){
            player.rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (Constants.PLAYER_FALL_SPEED_MULTIPLIER - 1) * Time.deltaTime;
        } else if (player.rigidBody.velocity.y > 0 && !Input.GetButton("Jump")){
            player.rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (Constants.PLAYER_JUMP_LOW_MULTIPLIER - 1) * Time.deltaTime;
        }
    }

    public void jump(){
        if(Input.GetButtonDown("Jump") && this.jumpCounter > 0 && jumpDelayOver){
            Vector2 force = new Vector2(0, player.getVerticalForce());
            
            this.player.rigidBody.AddForce(force, ForceMode2D.Impulse);
            
            this.jumpCounter -= 1;

            this.jumpDelayOver = false;
            player.StartCoroutine(delayJump());
        } else if (player.isGrounded && jumpDelayOver){
            this.jumpCounter = this.player.jumpCounter;
        }
    }

    private IEnumerator delayJump(){
        for (int i = 0; i < Constants.PLAYER_JUMP_DELAY; i++){
            yield return new WaitForFixedUpdate();
        }
        this.jumpDelayOver = true;
    }
}
