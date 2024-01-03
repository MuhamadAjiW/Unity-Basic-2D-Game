using UnityEngine;

public class PlayerAttackController{
    private Player player;

    public PlayerAttackController(Player player){
        this.player = player;
    }

    public void Execute(){
        if(Input.GetKeyDown(KeyCode.Z) && player.State != PlayerState.STANCE){
            if(player.Stamina < player.Weapon.StaminaCost) return;
            player.Weapon.Attack();
            player.Stamina -= player.Weapon.StaminaCost;
        }
    }
}