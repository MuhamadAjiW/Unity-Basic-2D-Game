using UnityEngine;

public static class Util{
    public static void PrintPlayerState(PlayerState state){
        switch (state)
        {
            case PlayerState.IDLE:
                Debug.Log("[PLAYER_STATE] Player is idle");
                break;
            case PlayerState.WALKING:
                Debug.Log("[PLAYER_STATE] Player is walking");
                break;
            case PlayerState.SPRINTING:
                Debug.Log("[PLAYER_STATE] Player is sprinting");
                break;
            case PlayerState.JUMPING:
                Debug.Log("[PLAYER_STATE] Player is jumping");
                break;
            case PlayerState.FALLING:
                Debug.Log("[PLAYER_STATE] Player is falling");
                break;
            case PlayerState.STANCE:
                Debug.Log("[PLAYER_STATE] Player is in stance");
                break;
            default:
                Debug.Log("[PLAYER_STATE] Invalid State");
                break;
        }
    }
}