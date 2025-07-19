using UnityEngine;

public class PlayerAnimationController : AnimationController
{
    private Player player;
    public PlayerAnimationController(Player player)
    {
        this.player = player;
        this.player.AddOnStateChange = HandleStateChange;
    }

    private void HandleStateChange()
    {
        Debug.Log("State changed");
        if (player.StateController.Heading == Direction.LEFT && player.transform.localScale.x > 0 ||
            player.StateController.Heading == Direction.RIGHT && player.transform.localScale.x < 0
        )
        {
            Mirror(player.transform);
        }

        // TODO: Insert actual animation
        switch (player.State)
        {
            case (PlayerState.IDLE):
                break;
            case (PlayerState.STANCE):
                break;
            case (PlayerState.WALKING):
                break;
            case (PlayerState.JUMPING):
                break;
            case (PlayerState.FALLING):
                break;
            case (PlayerState.SPRINTING):
                break;
        }

    }
}