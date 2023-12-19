using System.Collections;
using UnityEngine;

public class PlayerMovement {
    private Player player;
    private int jumpCounter;
    private bool jumpDelayOver = true;
    
    public PlayerMovement(Player player){
        this.player = player;
        jumpCounter = player.JumpLimit;
    }

    public void Move(){
        float keyPress = player.State == PlayerState.STANCE? 0 : Input.GetAxisRaw("Horizontal");
        Vector2 velocity = new(player.Rigidbody.velocity.x, player.Rigidbody.velocity.y);

        Vector2 dampVelocity = Vector2.zero;
        if(keyPress == 0){
            velocity.x = 0;
        } else{
            float maxSpeed = player.MaxSpeed;
            velocity.x = keyPress * maxSpeed;
        }

        player.Rigidbody.velocity = Vector2.SmoothDamp(player.Rigidbody.velocity, velocity, ref dampVelocity, PlayerConfig.PLAYER_MOVEMENT_SMOOTHING);
        if (player.Rigidbody.velocity.y < 0){
            player.Rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (PlayerConfig.PLAYER_FALL_SPEED_MULTIPLIER - 1) * Time.deltaTime;
        } else if (player.Rigidbody.velocity.y > 0 && !Input.GetButton("Jump")){
            player.Rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (PlayerConfig.PLAYER_JUMP_LOW_MULTIPLIER - 1) * Time.deltaTime;
        }
    }

    public void Jump(){
        if(Input.GetButtonDown("Jump") && player.State != PlayerState.STANCE && jumpCounter > 0 && jumpDelayOver){
            float snapshotSpeed = Mathf.Abs(player.Rigidbody.velocity.x * 1.3f);
            player.SnapshotSpeed = Mathf.Abs(snapshotSpeed > PlayerConfig.PLAYER_JUMP_MINIMUM_SPEED?  snapshotSpeed : PlayerConfig.PLAYER_JUMP_MINIMUM_SPEED);

            Vector2 force = new Vector2(0, player.JumpForce);
            
            player.Rigidbody.AddForce(force, ForceMode2D.Impulse);
            
            jumpCounter -= 1;

            jumpDelayOver = false;
            player.StartCoroutine(DelayJump());
        } else if (player.Grounded && jumpDelayOver){
            jumpCounter = player.JumpLimit;
        }
    }

    private IEnumerator DelayJump(){
        for (int i = 0; i < PlayerConfig.PLAYER_JUMP_DELAY; i++){
            yield return new WaitForFixedUpdate();
        }
        jumpDelayOver = true;
    }
}
