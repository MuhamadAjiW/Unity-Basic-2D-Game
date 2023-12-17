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
        // Also account current speed
        if(Input.GetAxisRaw("Horizontal") != 0 && Input.GetKey(KeyCode.LeftShift)){
            this.state = PlayerState.SPRINTING;
            return PlayerState.SPRINTING;
        }
        // Also account current speed
        if(Input.GetAxisRaw("Horizontal") != 0){
            this.state = PlayerState.WALKING;
            return PlayerState.WALKING;
        }
        this.state = PlayerState.IDLE;
        return PlayerState.IDLE;
    }
}
