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
        
        Vector2 newPos;



        // Debug.Log(player.rigidBody.velocity.x);
        // if (player.rigidBody.velocity.x > 0 && keyPress < 0){
        //     Debug.Log("condition 1");
        //     newPos = new Vector2(keyPress * Time.fixedDeltaTime * player.getHorizontalForce() * 0.5f, 0);
        // } else if (player.rigidBody.velocity.x < 0 && keyPress > 0){
        //     Debug.Log("condition 2");
        //     newPos =  new Vector2(keyPress * Time.fixedDeltaTime * player.getHorizontalForce() * 0.5f, 0);
        // } else{
        //     newPos = new Vector2(keyPress * Time.fixedDeltaTime * player.getHorizontalForce(), 0);
        // }

        newPos = new Vector2(keyPress * Time.fixedDeltaTime * player.getHorizontalForce(), 0);
        setPlayerPosition(getPlayerPosition() + newPos);
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

    void Update(){
        if (player.rigidBody.velocity.y < 0){
            player.rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (Constants.PLAYER_FALL_SPEED_MULTIPLIER - 1) * Time.deltaTime;
        } else if (player.rigidBody.velocity.y > 0 && !Input.GetButton("Jump")){
            player.rigidBody.velocity += Vector2.up * Physics2D.gravity.y * (Constants.PLAYER_JUMP_LOW_MULTIPLIER - 1) * Time.deltaTime;
        }
    }
}
