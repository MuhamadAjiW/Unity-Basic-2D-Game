using UnityEngine;

public class PlayerStance{
    private Player player;
    public float dashModifier = 10;

    public PlayerStance(Player player){
        this.player = player;
    }
    

    private void Dash(){
        Vector2 newPos;
        if(Input.GetKey(KeyCode.UpArrow))           newPos = new Vector2(0, dashModifier);
        else if(Input.GetKey(KeyCode.RightArrow))   newPos = new Vector2(dashModifier, 0);
        else if(Input.GetKey(KeyCode.DownArrow))    newPos = new Vector2(0, (-1) * dashModifier);
        else if(Input.GetKey(KeyCode.LeftArrow))    newPos = new Vector2((-1) * dashModifier, 0);
        else                                        return;

        if(Input.GetKeyDown(KeyCode.LeftShift)){
            Debug.Log("Dashed");
            player.transform.position = player.GetPosition() + newPos;
        }

    }

    public void Execute(){
        if(player.GetPlayerState() != PlayerState.STANCE) return;
        Dash();
    }
}