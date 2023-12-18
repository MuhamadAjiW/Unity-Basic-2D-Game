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

    public void Move(){
        Vector2 force = new Vector2(0,0);
        float playerHorizontalForce = player.GetHorizontalForce();
        Vector2 velocity = player.GetRigidbody2D().velocity;
        PlayerState playerState = player.getPlayerState();
        float keyPress = playerState == PlayerState.STANCE? 0 : Input.GetAxis("Horizontal");

        if(keyPress == 0){
            if(player.IsGrounded()){
                velocity.x = Mathf.Lerp(velocity.x, 0, 0.05f * Constants.PLAYER_MOVEMENT_SMOOTHING);
            } else{
                velocity.x = Mathf.Lerp(velocity.x, 0, 0.02f * Constants.PLAYER_MOVEMENT_SMOOTHING);
            }
            player.GetRigidbody2D().velocity = velocity;
        } else{
            force = new Vector2(keyPress * playerHorizontalForce, 0);

            float maxSpeed = player.GetMaxSpeed();
            switch (playerState)
            {
                case (PlayerState.SPRINTING):
                case (PlayerState.WALKING):
                    if(keyPress > 0 && velocity.x > maxSpeed){
                        velocity.x = Mathf.Lerp(velocity.x, maxSpeed, Time.fixedDeltaTime * Constants.PLAYER_MOVEMENT_SMOOTHING);
                        player.GetRigidbody2D().velocity = velocity;
                        break;
                    }
                    else if(keyPress < 0 && velocity.x < (-1) * maxSpeed){
                        velocity.x = Mathf.Lerp(velocity.x, (-1) * maxSpeed, Time.fixedDeltaTime * Constants.PLAYER_MOVEMENT_SMOOTHING);
                        player.GetRigidbody2D().velocity = velocity;
                        break;
                    }
                    player.GetRigidbody2D().AddForce(force, ForceMode2D.Impulse);
                    break;
                case (PlayerState.JUMPING):
                case (PlayerState.FALLING):
                    if(keyPress > 0 && velocity.x > maxSpeed){
                        break;
                    }
                    else if(keyPress < 0 && velocity.x < maxSpeed){
                        break;
                    }
                    player.GetRigidbody2D().AddForce(force, ForceMode2D.Impulse);
                    break;
            }
        }

        if (player.GetRigidbody2D().velocity.y < 0){
            player.GetRigidbody2D().velocity += Vector2.up * Physics2D.gravity.y * (Constants.PLAYER_FALL_SPEED_MULTIPLIER - 1) * Time.deltaTime;
        } else if (player.GetRigidbody2D().velocity.y > 0 && !Input.GetButton("Jump")){
            player.GetRigidbody2D().velocity += Vector2.up * Physics2D.gravity.y * (Constants.PLAYER_JUMP_LOW_MULTIPLIER - 1) * Time.deltaTime;
        }
    }

    public void Jump(){
        if(Input.GetButtonDown("Jump") && player.getPlayerState() != PlayerState.STANCE && jumpCounter > 0 && jumpDelayOver){
            Vector2 force = new Vector2(0, player.GetVerticalForce());
            
            player.GetRigidbody2D().AddForce(force, ForceMode2D.Impulse);
            
            jumpCounter -= 1;

            jumpDelayOver = false;
            player.StartCoroutine(DelayJump());
        } else if (player.IsGrounded() && jumpDelayOver){
            jumpCounter = player.jumpCounter;
        }
    }

    private IEnumerator DelayJump(){
        for (int i = 0; i < Constants.PLAYER_JUMP_DELAY; i++){
            yield return new WaitForFixedUpdate();
        }
        jumpDelayOver = true;
    }
}
