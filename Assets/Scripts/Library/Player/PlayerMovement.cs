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
        PlayerState playerState = player.GetPlayerState();
        float keyPress = playerState == PlayerState.STANCE? 0 : Input.GetAxisRaw("Horizontal");
        Vector2 velocity = new(player.GetRigidbody2D().velocity.x, player.GetRigidbody2D().velocity.y);

        Vector2 dampVelocity = Vector2.zero;
        if(keyPress == 0){
            velocity.x = 0;
            player.GetRigidbody2D().velocity = Vector2.SmoothDamp(player.GetRigidbody2D().velocity, velocity, ref dampVelocity, Constants.PLAYER_MOVEMENT_SMOOTHING);
        } else{
            float maxSpeed = player.GetMaxSpeed();
            switch (playerState)
            {
                case (PlayerState.SPRINTING):
                case (PlayerState.WALKING):
                    velocity.x = keyPress * maxSpeed;
                    player.GetRigidbody2D().velocity = Vector2.SmoothDamp(player.GetRigidbody2D().velocity, velocity, ref dampVelocity, Constants.PLAYER_MOVEMENT_SMOOTHING);
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
        if(Input.GetButtonDown("Jump") && player.GetPlayerState() != PlayerState.STANCE && jumpCounter > 0 && jumpDelayOver){
            Vector2 force = new Vector2(0, player.GetJumpForce());
            
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
