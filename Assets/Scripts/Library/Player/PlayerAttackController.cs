using UnityEngine;
using static ConfigurationManager;
 
public class PlayerAttackController
{

    private Player player;
 
    public PlayerAttackController(Player player)
    {
        this.player = player;
    }
 
    public void Execute()
    {
        if (Instance != null && Instance.controlsConfig != null && Input.GetKeyDown(Instance.controlsConfig.Attack) && player.State != PlayerState.STANCE)
        {
            if (player.Stamina < player.Weapon.StaminaCost) return;
            player.Weapon.Attack();
            player.Stamina -= player.Weapon.StaminaCost;
        }
    }
}