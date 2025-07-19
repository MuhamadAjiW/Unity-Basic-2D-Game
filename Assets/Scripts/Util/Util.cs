using UnityEngine;

public static class Util{
    public static void PrintPlayerState(PlayerStateController controller){
        string state = "UNKNOWN";
        string direction = "UNKNOWN";

        switch(controller.Heading){
            case Direction.RIGHT:
                direction = "right";
                break;
            case Direction.LEFT:
                direction = "left";
                break;
        }

        switch (controller.State){
            case PlayerState.IDLE:
                state = "idle";
                break;
            case PlayerState.WALKING:
                state = "walking";
                break;
            case PlayerState.SPRINTING:
                state = "sprinting";
                break;
            case PlayerState.JUMPING:
                state = "jumping";
                break;
            case PlayerState.FALLING:
                state = "falling";
                break;
            case PlayerState.STANCE:
                state = "stance";
                break;
        }

        Debug.Log(string.Format("[PLAYER STATE] state: {0}; heading: {1}", state, direction));
    }
}