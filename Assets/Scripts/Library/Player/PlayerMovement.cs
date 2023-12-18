using System.Collections;
using UnityEngine;

public class PlayerMovement {
    private Player player;
    private int jumpCounter;
    private bool jumpDelayOver = true;
    
    public PlayerMovement(Player player){
        this.player = player;
        jumpCounter = player.GetJumpLimit();
    }

    public void Move(){
        PlayerState playerState = player.GetPlayerState();
        float keyPress = playerState == PlayerState.STANCE? 0 : Input.GetAxisRaw("Horizontal");
        Vector2 velocity = new(player.GetRigidbody2D().velocity.x, player.GetRigidbody2D().velocity.y);

        Vector2 dampVelocity = Vector2.zero;
        if(keyPress == 0){
            velocity.x = 0;
        } else{
            float maxSpeed = player.GetMaxSpeed();
            velocity.x = keyPress * maxSpeed;
        }

        player.GetRigidbody2D().velocity = Vector2.SmoothDamp(player.GetRigidbody2D().velocity, velocity, ref dampVelocity, PlayerConfig.PLAYER_MOVEMENT_SMOOTHING);
        if (player.GetRigidbody2D().velocity.y < 0){
            player.GetRigidbody2D().velocity += Vector2.up * Physics2D.gravity.y * (PlayerConfig.PLAYER_FALL_SPEED_MULTIPLIER - 1) * Time.deltaTime;
        } else if (player.GetRigidbody2D().velocity.y > 0 && !Input.GetButton("Jump")){
            player.GetRigidbody2D().velocity += Vector2.up * Physics2D.gravity.y * (PlayerConfig.PLAYER_JUMP_LOW_MULTIPLIER - 1) * Time.deltaTime;
        }
    }

    public void Jump(){
        if(Input.GetButtonDown("Jump") && player.GetPlayerState() != PlayerState.STANCE && jumpCounter > 0 && jumpDelayOver){
            float snapshotSpeed = Mathf.Abs(player.GetRigidbody2D().velocity.x * 1.3f);
            player.SetSnapshotSpeed(Mathf.Abs(snapshotSpeed > PlayerConfig.PLAYER_JUMP_MINIMUM_SPEED?  snapshotSpeed : PlayerConfig.PLAYER_JUMP_MINIMUM_SPEED));

            Vector2 force = new Vector2(0, player.GetJumpForce());
            
            player.GetRigidbody2D().AddForce(force, ForceMode2D.Impulse);
            
            jumpCounter -= 1;

            jumpDelayOver = false;
            player.StartCoroutine(DelayJump());
        } else if (player.IsGrounded() && jumpDelayOver){
            jumpCounter = player.GetJumpLimit();
        }
    }

    private IEnumerator DelayJump(){
        for (int i = 0; i < PlayerConfig.PLAYER_JUMP_DELAY; i++){
            yield return new WaitForFixedUpdate();
        }
        jumpDelayOver = true;
    }
}
