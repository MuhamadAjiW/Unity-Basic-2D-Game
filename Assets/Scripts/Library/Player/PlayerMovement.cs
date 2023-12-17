using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement {
    private Player player; 
    
    public PlayerMovement(Player player){
        this.player = player;
    }

    private Vector2 getPlayerPosition(){
        return player.transform.position;
    }

    private void setPlayerPosition(Vector2 newPosition){
        player.transform.position = newPosition;
    }

    public void move(){
        float keyPress = Input.GetAxisRaw("Horizontal");

        Vector2 newPos = new Vector2(keyPress * Time.fixedDeltaTime * player.getHorizontalForce(), 0);

        setPlayerPosition(getPlayerPosition() + newPos);
    }

    public void jump(){
        float keyPress = Input.GetAxisRaw("Jump");
        
        Vector2 newPos = new Vector2(0, keyPress * Time.fixedDeltaTime * player.getVerticalForce());

        setPlayerPosition(getPlayerPosition() + newPos);
    }
}
