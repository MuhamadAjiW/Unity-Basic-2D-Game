using UnityEngine;
 
public class PlayerAttackController
{
    [SerializeField] private ControlsConfig controlsConfig; // Reference to the ScriptableObject

    private Player player;
 
    public PlayerAttackController(Player player)
    {
        this.player = player;
    }
 
    public void Execute()
    {
        if (controlsConfig != null && Input.GetKeyDown(controlsConfig.Attack) && player.State != PlayerState.STANCE)
        {
            if (player.Stamina < player.Weapon.StaminaCost) return;
            player.Weapon.Attack();
            player.Stamina -= player.Weapon.StaminaCost;
        }
    }
}