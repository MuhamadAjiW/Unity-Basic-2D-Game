using System;
using UnityEngine;

public class Dummy : EnemyObject{
    private DummyStateController stateController;
    private new void Awake(){
        base.Awake();
        Health = baseHealth * EnemyConfig.ENEMY_GLOBAL_HEALTH_MULTIPLIER;
        SpriteRenderer.color = Color.gray;
        stateController = new DummyStateController(this);
        OnDeath += Death;
        stateController.OnDamageDelayOver += DamageCleared;
    }

    private void Death(){
        Debug.Log("Dummy is dead");
    }

    private void DamageCleared(){
        if(!Dead){
            SpriteRenderer.color = Color.gray;
            Debug.Log("Dummy is no longer damaged");
        }
    }

    public override float InflictDamage(float damage){
        if(!Dead && !stateController.Damaged){
            SpriteRenderer.color = Color.red;

            Health -= damage;
            InvokeOnDamaged();
            if(Dead) InvokeOnDeath();
            Debug.Log(string.Format("Dummy remaining health: {0}", Health));
        }
        return Health;
    }

    void Update(){
        refresh();
    }

    void FixedUpdate(){
        // Util.PrintPlayerState(stateController);
        stateController.UpdateState();
    }

}
