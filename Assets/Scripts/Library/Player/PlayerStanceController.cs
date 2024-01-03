using UnityEditor;
using UnityEngine;

public class PlayerStanceController{
    private Player player;
    private float dashRange = 10;
    private float dashCost = 50;
    private LayerMask ignored;

    public float DashRange{
        get { return dashRange; }
        set { dashRange = value > 0 ? value : 0; }
    }

    public float DashCost{
        get { return dashCost; }
        set { dashCost = value > 0 ? value : 0; }
    }

    public PlayerStanceController(Player player, LayerMask ignored){
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
            if(player.Stamina < DashCost) return;

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

            Vector2 scaleModifier = new Vector2(1,1);
            scaleModifier.x *= dashDistance/dashRange;

            //TODO: Weapon adjustment for damage and knockback power
            GameObject dashTrail = ObjectManager.GenerateAttackObject(
                PrefabsPath.PREFAB_PLAYER_DASH,
                knockbackDirection: direction,
                knockbackPower: player.Weapon.KnockbackPower,
                damage: player.Weapon.Damage,
                isPlayer: true,
                position: trailPosition,
                scaleModifier: scaleModifier,
                rotation: trailRotation,
                name: "Dashtrail",
                sortingOrder: -1000
            );
            
            ObjectManager.Destroy(dashTrail, PlayerConfig.DASH_TRAIL_DURATION);

            player.Stamina -= DashCost;
            player.transform.position = player.Position + dashVector * dashDistance;
        }

    }

    public void Execute(){
        if(player.State != PlayerState.STANCE) return;
        Dash();
    }
}