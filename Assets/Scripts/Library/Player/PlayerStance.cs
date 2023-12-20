using UnityEditor;
using UnityEngine;

public class PlayerStance{
    private Player player;
    public float dashRange = 10;
    private LayerMask ignored;

    public PlayerStance(Player player, LayerMask ignored){
        this.player = player;
        this.ignored = ignored;
    }
    
    private float DetectDash(Vector2 direction){
        RaycastHit2D hit = Physics2D.Raycast(player.Position, direction, dashRange, ~ignored);
        Debug.DrawRay(player.Position, direction * dashRange, Color.red);

        if (hit.collider != null){
            float distanceToObject = hit.distance;
            // Debug.Log("Blocking object detected at a distance of: " + distanceToObject + " units.");
            return distanceToObject - 0.1f;
        }
        else {
            // Debug.Log("No object detected.");
            return dashRange;
        }
    }


    private void Dash(){
        Direction direction = Direction.NULL;
        if(Input.GetKey(KeyCode.UpArrow))           direction = Direction.UP;
        else if(Input.GetKey(KeyCode.RightArrow))   direction = Direction.RIGHT;
        else if(Input.GetKey(KeyCode.DownArrow))    direction = Direction.DOWN;
        else if(Input.GetKey(KeyCode.LeftArrow))    direction = Direction.LEFT;
        else                                        return;

        Vector2 dashVector;
        switch (direction){
            case Direction.UP: dashVector = Vector2.up; break;
            case Direction.RIGHT: dashVector = Vector2.right; break;
            case Direction.DOWN: dashVector = Vector2.down; break;
            case Direction.LEFT: dashVector = Vector2.left; break;
            default: Debug.LogError("Undefined direction"); return;
        }

        float dashDistance = DetectDash(dashVector);
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            Vector2 trailPosition = player.Position;
            Quaternion? trailRotation = null;
            switch (direction){
                case Direction.UP:
                    trailRotation = Quaternion.Euler(0,0,90f);
                    trailPosition.y += dashDistance / 2;
                    break;
                case Direction.RIGHT:
                    trailPosition.x += dashDistance / 2;
                    break;
                case Direction.DOWN:
                    trailRotation = Quaternion.Euler(0,0,90f);
                    trailPosition.y -= dashDistance / 2;
                    break;
                case Direction.LEFT:
                    trailPosition.x -= dashDistance / 2;
                    break;
                default:
                    Debug.LogError("Undefined dash properties");
                    return;
            }

            //TODO: Weapon adjustment for damage and knockback power
            GameObject dashTrail = ObjectManager.GenerateAttackObject(
                PrefabsPath.PREFAB_PLAYER_DASH,
                knockbackDirection: direction,
                knockbackPower: 500,
                damage: 10,
                isPlayer: true,

                position: trailPosition,
                rotation: trailRotation,
                name: "Dashtrail",
                sortingOrder: -1000
            );
            
            ObjectManager.Destroy(dashTrail, PlayerConfig.DASH_TRAIL_DURATION);

            player.transform.position = player.Position + dashVector * dashDistance;
        }

    }

    public void Execute(){
        if(player.State != PlayerState.STANCE) return;
        Dash();
    }
}