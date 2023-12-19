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
        Vector2 dashVector;
        if(Input.GetKey(KeyCode.UpArrow))           dashVector = Vector2.up;
        else if(Input.GetKey(KeyCode.RightArrow))   dashVector = Vector2.right;
        else if(Input.GetKey(KeyCode.DownArrow))    dashVector = Vector2.down;
        else if(Input.GetKey(KeyCode.LeftArrow))    dashVector = Vector2.left;
        else                                        return;

        float dashDistance = DetectDash(dashVector);
        if(Input.GetKeyDown(KeyCode.LeftShift)){
            // Debug.Log("Dashed");

            GameObject gameObject = new GameObject();
            gameObject.name = "gameObject";
            SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            Sprite tileBlankSprite = Resources.Load<Sprite>("Graphics/Misc/TileBlank");

            if (tileBlankSprite != null) {
                spriteRenderer.sprite = tileBlankSprite;
            } else {
                Debug.LogError("Sprite not found.");
            }

            player.transform.position = player.Position + dashVector * dashDistance;
        }

    }

    public void Execute(){
        if(player.State != PlayerState.STANCE) return;
        Dash();
    }
}