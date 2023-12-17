using UnityEngine;

public class PlayerStateController{

    public Player player;
    private PlayerState state;

    public PlayerStateController(Player player){
        this.player = player;
    }


    public PlayerState GetState(){
        return this.state;
    }

    public PlayerState updateState(){
        if(Input.GetKey(KeyCode.LeftShift)){
            this.state = PlayerState.STANCE;
            return PlayerState.STANCE;
        }
        // Also account current speed
        else if(Input.GetAxisRaw("Horizontal") != 0 && Input.GetKey(KeyCode.LeftShift)){
            this.state = PlayerState.SPRINTING;
            return PlayerState.SPRINTING;
        }
        // Also account current speed
        else if(Input.GetAxisRaw("Horizontal") != 0){
            this.state = PlayerState.WALKING;
            return PlayerState.WALKING;
        }
        else if(!player.isGrounded && player.rigidBody.velocity.y > 0){
            this.state = PlayerState.JUMPING;
            return PlayerState.JUMPING;
        }
        else if(!player.isGrounded && player.rigidBody.velocity.y < 0){
            this.state = PlayerState.FALLING;
            return PlayerState.JUMPING;
        }
        this.state = PlayerState.IDLE;
        return PlayerState.IDLE;
    }
}
