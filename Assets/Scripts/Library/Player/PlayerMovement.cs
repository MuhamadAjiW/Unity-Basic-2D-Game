using System.Collections;
using UnityEngine;

public class PlayerMovement {
    private Player player;
    private int jumpCounter;
    private bool jumpDelayOver = true;
    
    public PlayerMovement(Player player){
        this.player = player;
        jumpCounter = player.jumpCounter;
    }

    private Vector2 getPlayerPosition(){
        return player.transform.position;
    }

    private void setPlayerPosition(Vector2 newPosition){
        player.transform.position = newPosition;
    }

    public void move(){
        Vector2 force = new Vector2(0,0);
        float playerHorizontalForce = player.getHorizontalForce();
        Vector2 velocity = player.rigidBody.velocity;
        PlayerState playerState = player.getPlayerState();
        float keyPress = playerState == PlayerState.STANCE? 0 : Input.GetAxis("Horizontal");

        if(keyPress == 0){
            if(player.isGrounded){
                velocity.x = Mathf.Lerp(velocity.x, 0, 0.05f * Constants.PLAYER_MOVEMENT_SMOOTHING);
            } else{
                velocity.x = Mathf.Lerp(velocity.x, 0, 0.02f * Constants.PLAYER_MOVEMENT_SMOOTHING);
            }
            player.rigidBody.velocity = velocity;
        } else{
            force = new Vector2(keyPress * playerHorizontalForce, 0);

            switch (playerState)
            {
                case (PlayerState.WALKING):
                    if(keyPress > 0 && velocity.x > player.walkSpeed){
                        velocity.x = Mathf.Lerp(velocity.x, player.walkSpeed, Time.fixedDeltaTime * Constants.PLAYER_MOVEMENT_SMOOTHING);
                        player.rigidBody.velocity = velocity;
                        break;
                    }
                    else if(keyPress < 0 && velocity.x < (-1) * player.walkSpeed){
                        velocity.x = Mathf.Lerp(velocity.x, (-1) * player.walkSpeed, Time.fixedDeltaTime * Constants.PLAYER_MOVEMENT_SMOOTHING);
                        player.rigidBody.velocity = velocity;
                        break;
                    }
                    player.rigidBody.AddForce(force, ForceMode2D.Impulse);
                    break;
                case (PlayerState.SPRINTING):
                    if(keyPress > 0 && velocity.x > player.sprintSpeed){
                        velocity.x = Mathf.Lerp(velocity.x, player.sprintSpeed, Time.fixedDeltaTime * Constants.PLAYER_MOVEMENT_SMOOTHING);
                        player.rigidBody.velocity = velocity;
                        break;
                    }
                    else if(keyPress < 0 && velocity.x < (-1) * player.sprintSpeed){
                        velocity.x = Mathf.Lerp(velocity.x, (-1) * player.sprintSpeed, Time.fixedDeltaTime * Constants.PLAYER_MOVEMENT_SMOOTHING);
                        player.rigidBody.velocity = velocity;
                        break;
                    }
                    player.rigidBody.AddForce(force, ForceMode2D.Impulse);
                    break;
                case (PlayerState.JUMPING):
                case (PlayerState.FALLING):
                    if(keyPress > 0 && velocity.x > 0){
                        break;
                    }
                    else if(keyPress < 0 && velocity.x < 0){
                        break;
                    }
                    player.rigidBody.AddForce(force, ForceMode2D.Impulse);
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
        if(Input.GetButtonDown("Jump") && player.getPlayerState() != PlayerState.STANCE && jumpCounter > 0 && jumpDelayOver){
            Vector2 force = new Vector2(0, player.getVerticalForce());
            
            player.rigidBody.AddForce(force, ForceMode2D.Impulse);
            
            jumpCounter -= 1;

            jumpDelayOver = false;
            player.StartCoroutine(delayJump());
        } else if (player.isGrounded && jumpDelayOver){
            jumpCounter = player.jumpCounter;
        }
    }

    private IEnumerator delayJump(){
        for (int i = 0; i < Constants.PLAYER_JUMP_DELAY; i++){
            yield return new WaitForFixedUpdate();
        }
        jumpDelayOver = true;
    }
}
