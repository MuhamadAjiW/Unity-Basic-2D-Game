using UnityEngine;

public class PlayerStateController{

    public Player player;
    private PlayerState state;

    public PlayerStateController(Player player){
        this.player = player;
    }


    public PlayerState getState(){
        return state;
    }

    public PlayerState updateState(){
        if(Input.GetKey(KeyCode.LeftControl)){
            state = PlayerState.STANCE;
            return PlayerState.STANCE;
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
        else if(!player.grounded && player.GetRigidbody2D().velocity.y > 0){
            state = PlayerState.JUMPING;
            return PlayerState.JUMPING;
        }
        else if(!player.grounded && player.GetRigidbody2D().velocity.y < 0){
            state = PlayerState.FALLING;
            return PlayerState.JUMPING;
        }
        state = PlayerState.IDLE;
        return PlayerState.IDLE;
    }
}
