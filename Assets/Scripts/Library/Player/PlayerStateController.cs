using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateController : DamageableEntityStateController<Player> {

    private Player player;
    public PlayerStateController(Player player) : base(player){
        this.player = player;
    }

    public override void OnDamaged(){
        damaged = true;
        player.StartCoroutine(DamagedDelay());
    }
    public override void OnDeath(){}


    public override int UpdateState(){
        if(Input.GetKey(KeyCode.LeftControl)){
            state = PlayerState.STANCE;
            return PlayerState.STANCE;
        }
        else if(!player.IsGrounded && player.GetRigidbody2D().velocity.y > 0){
            state = PlayerState.JUMPING;
            return PlayerState.JUMPING;
        }
        else if(!player.IsGrounded && player.GetRigidbody2D().velocity.y < 0){
            state = PlayerState.FALLING;
            return PlayerState.JUMPING;
        }
        // Also account current speed
        else if(Input.GetAxisRaw("Horizontal") != 0 && Input.GetKey(KeyCode.LeftShift)){
            state = PlayerState.SPRINTING;
            this.heading = Input.GetAxisRaw("Horizontal") == 1? HeadingDirection.RIGHT : HeadingDirection.LEFT;
            return PlayerState.SPRINTING;
        }
        // Also account current speed
        else if(Input.GetAxisRaw("Horizontal") != 0){
            state = PlayerState.WALKING;
            this.heading = Input.GetAxisRaw("Horizontal") == 1? HeadingDirection.RIGHT : HeadingDirection.LEFT;
            return PlayerState.WALKING;
        }
        state = PlayerState.IDLE;
        return PlayerState.IDLE;
    }

    private IEnumerator DamagedDelay(){
        if(!player.IsDead()){
            yield return new WaitForSeconds(PlayerConfig.PLAYER_DAMAGED_STATE_DURATION);
            damaged = false;
            invokeDamageDelayOver();
        }
    }
}
