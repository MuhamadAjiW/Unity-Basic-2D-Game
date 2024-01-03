using UnityEngine;

public class TestWeapon : WeaponObject{
    private const string ATTACK_TRIGGER = "Attacking"; 
    private const string STOP_TRIGGER = "Stopped"; 

    public Collider2D hitbox;
    public bool attackInput = false;
    public bool finalAttack = false;

    public bool FinalAttack{
        get {return finalAttack;}
        set {finalAttack = value;}
    }

    new void Awake(){
        base.Awake();
        hitbox = transform.GetComponentInChildren<Collider2D>();
        hitbox.enabled = false;
    }

    public override void Attack(){
        if(!attackInput && !finalAttack){
            attackInput = true;
            animator.SetBool(ATTACK_TRIGGER, true);
            animator.SetBool(STOP_TRIGGER, false);
        }
    }

    public void AttackDone(){
        Debug.Log("Attack is done");
        hitbox.enabled = false;
        finalAttack = false;
        attackInput = false;
        animator.SetBool(STOP_TRIGGER, true);
    }

    public void AttackCalled(){
        hitbox.enabled = true;
        attackInput = false;
        animator.SetBool(ATTACK_TRIGGER, false);
    }

    public void FinalAttackCalled(){
        finalAttack = true;
        AttackCalled();
    }
}