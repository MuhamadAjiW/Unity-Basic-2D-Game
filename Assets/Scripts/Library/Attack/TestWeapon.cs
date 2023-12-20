using UnityEngine;

public class TestWeapon : WeaponObject{
    private const string ATTACK_TRIGGER = "Attacking"; 
    private const string STOP_TRIGGER = "Stopped"; 

    private bool attackInput = false;

    public override void Attack(){
        if(!attackInput){
            attackInput = true;
            animator.SetBool(ATTACK_TRIGGER, true);
            animator.SetBool(STOP_TRIGGER, false);
        }
    }

    public void AttackDone(){
        animator.SetBool(STOP_TRIGGER, true);
    }

    public void AttackCalled(){
        attackInput = false;
        animator.SetBool(ATTACK_TRIGGER, false);
    }
}