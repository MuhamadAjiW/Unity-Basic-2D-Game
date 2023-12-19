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
        char key = 'n';
        if(Input.GetKey(KeyCode.UpArrow))           key = 'w';
        else if(Input.GetKey(KeyCode.RightArrow))   key = 'd';
        else if(Input.GetKey(KeyCode.DownArrow))    key = 's';
        else if(Input.GetKey(KeyCode.LeftArrow))    key = 'a';
        else                                        return;

        Vector2 dashVector;
        switch (key){
            case 'w': dashVector = Vector2.up; break;
            case 'd': dashVector = Vector2.right; break;
            case 's': dashVector = Vector2.down; break;
            case 'a': dashVector = Vector2.left; break;
            default: Debug.LogError("Undefined dashVector"); return;
        }

        float dashDistance = DetectDash(dashVector);
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            // Debug.Log("Dashed");

            // GameObject gameObject = new GameObject();
            // gameObject.name = "gameObject";
            // SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            // Sprite tileBlankSprite = Resources.Load<Sprite>("Graphics/Misc/TileBlank");

            // if (tileBlankSprite != null) {
            //     spriteRenderer.sprite = tileBlankSprite;
            // } else {
            //     Debug.LogError("Sprite not found.");
            // }

            Vector2 trailPosition = player.Position;
            Quaternion? trailRotation = null;
            switch (key){
                case 'w':
                    trailRotation = Quaternion.Euler(0,0,90f);
                    trailPosition.y += dashDistance / 2;
                    break;
                case 'd':
                    trailPosition.x += dashDistance / 2;
                    break;
                case 's':
                    trailRotation = Quaternion.Euler(0,0,90f);
                    trailPosition.y -= dashDistance / 2;
                    break;
                case 'a':
                    trailPosition.x -= dashDistance / 2;
                    break;
                default:
                    Debug.LogError("Undefined dash properties");
                    return;
            }

            GameObject dashTrail = ObjectController.Generate(
                PrefabsPath.PREFAB_PLAYER_DASH,
                position: trailPosition,
                rotation: trailRotation,
                name: "Dashtrail",
                sortingOrder: -1000
            );
            
            ObjectController.Destroy(dashTrail, PlayerConfig.PLAYER_DASH_TRAIL_DURATION);

            player.transform.position = player.Position + dashVector * dashDistance;
        }

    }

    public void Execute(){
        if(player.State != PlayerState.STANCE) return;
        Dash();
    }
}