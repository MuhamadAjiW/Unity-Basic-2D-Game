using System;
using UnityEngine;

public class Dummy : EnemyObject{
    private DummyStateController stateController;
    public override bool Damageable => !Dead && !stateController.Damaged;
    private new void Awake(){
        base.Awake();
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
        SpriteRenderer.color = Color.red;
        base.InflictDamage(damage);

        return Health;
    }

    void Update(){
        Refresh();
        Smoothen();
    }

    void FixedUpdate(){
        // Util.PrintPlayerState(stateController);
        stateController.UpdateState();
    }

}
