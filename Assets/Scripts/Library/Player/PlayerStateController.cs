using UnityEngine;

public class PlayerStateController{

    public Player player;
    private PlayerState state;

    public PlayerStateController(Player player){
        this.player = player;
    }


    public PlayerState GetState(){
        return state;
    }

    public PlayerState UpdateState(){
        if(Input.GetKey(KeyCode.LeftControl)){
            state = PlayerState.STANCE;
            return PlayerState.STANCE;
        }
        else if(!player.IsGrounded() && player.GetRigidbody2D().velocity.y > 0){
            state = PlayerState.JUMPING;
            return PlayerState.JUMPING;
        }
        else if(!player.IsGrounded() && player.GetRigidbody2D().velocity.y < 0){
            state = PlayerState.FALLING;
            return PlayerState.JUMPING;
        }
        // Also account current speed
        else if(Input.GetAxisRaw("Horizontal") != 0 && Input.GetKey(KeyCode.LeftShift)){
            state = PlayerState.SPRINTING;
            return PlayerState.SPRINTING;
        }
        // Also account current speed
        else if(Input.GetAxisRaw("Horizontal") != 0){
            state = PlayerState.WALKING;
            return PlayerState.WALKING;
        }
        state = PlayerState.IDLE;
        return PlayerState.IDLE;
    }
}
