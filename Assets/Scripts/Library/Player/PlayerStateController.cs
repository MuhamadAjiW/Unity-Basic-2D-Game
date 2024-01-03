using System.Collections;
using UnityEngine;

public class PlayerStateController : DamageableEntityStateController<Player> {

    private Player player;
    public PlayerStateController(Player player) : base(player){
        this.player = player;
        OnDamageDelayOver += DamageCleared;
    }

    protected override void OnDamaged(){
        damaged = true;
        InvokeOnStateChanged();
        player.StartCoroutine(DamagedDelay());
    }
    protected override void OnDeath(){
        GameController.MainCamera.ResetCameraBehaviour();
        Debug.Log("Player is dead");
        GameObject.Destroy(player);
    }

    public override int UpdateState(){
        int initialState = state;
        Direction initialHeading = heading;
        if(Input.GetKey(KeyCode.LeftControl)){
            state = PlayerState.STANCE;
        }
        else if(!player.Grounded && player.Rigidbody.velocity.y > 0){
            state = PlayerState.JUMPING;
        }
        else if(!player.Grounded && player.Rigidbody.velocity.y < 0){
            state = PlayerState.FALLING;
        }
        // Also account current speed
        else if(Input.GetAxisRaw("Horizontal") != 0 && Input.GetKey(KeyCode.LeftShift)){
            state = PlayerState.SPRINTING;
            heading = Input.GetAxisRaw("Horizontal") == 1? Direction.RIGHT : Direction.LEFT;
        }
        // Also account current speed
        else if(Input.GetAxisRaw("Horizontal") != 0){
            state = PlayerState.WALKING;
            heading = Input.GetAxisRaw("Horizontal") == 1? Direction.RIGHT : Direction.LEFT;
        }
        else{
            state = PlayerState.IDLE;
        }

        if(initialHeading != heading || initialState != state){
            InvokeOnStateChanged();
        }

        return state;
    }

    private IEnumerator DamagedDelay(){
        if(!player.Dead){
            yield return new WaitForSeconds(PlayerConfig.DAMAGED_STATE_DURATION);
            damaged = false;
            invokeDamageDelayOver();
        }
    }

    private void DamageCleared(){
        if(!player.Dead){
            InvokeOnStateChanged();
            player.SpriteRenderer.color = Color.white;
            Debug.Log("Player is no longer damaged");
        }
    }
}
