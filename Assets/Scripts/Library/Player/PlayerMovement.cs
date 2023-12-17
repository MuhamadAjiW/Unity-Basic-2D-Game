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
        Debug.Log(this.jumpCounter);
    }

    private Vector2 getPlayerPosition(){
        return player.transform.position;
    }

    private void setPlayerPosition(Vector2 newPosition){
        player.transform.position = newPosition;
    }

    public void move(){
        float keyPress = Input.GetAxis("Horizontal");

        Vector2 newPos = new Vector2(keyPress * Time.fixedDeltaTime * player.getHorizontalForce(), 0);

        setPlayerPosition(getPlayerPosition() + newPos);
    }

    public void jump(){
        if(Input.GetButtonDown("Jump") && this.jumpCounter > 0 && jumpDelayOver){
            Debug.Log(this.jumpCounter);
            Vector2 force = new Vector2(0, player.getVerticalForce());
            
            this.player.rigidBody.AddForce(force, ForceMode2D.Impulse);
            
            this.jumpCounter -= 1;

            this.jumpDelayOver = false;
            player.StartCoroutine(delayJump());
        } else if (player.isGrounded && jumpDelayOver){
            Debug.Log(String.Format("grounded jumpCounter: {0}", this.jumpCounter));
            this.jumpCounter = this.player.jumpCounter;
        }
    }

    private IEnumerator delayJump(){
        for (int i = 0; i < 4; i++){
            yield return new WaitForFixedUpdate();
        }
        this.jumpDelayOver = true;
        Debug.Log(String.Format("Jump delay over"));
    }
}
