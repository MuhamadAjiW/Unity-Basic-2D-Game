using UnityEngine;

public class TestWeapon : WeaponObject{
    private const string ATTACK_TRIGGER = "Attacking"; 
    private const string STOP_TRIGGER = "Stopped"; 

    public bool attackInput = false;
    
    public bool finalAttack = false;

    public bool FinalAttack{
        get {return finalAttack;}
        set {finalAttack = value;}
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
        finalAttack = false;
        attackInput = false;
        animator.SetBool(STOP_TRIGGER, true);
    }

    public void AttackCalled(){
        attackInput = false;
        animator.SetBool(ATTACK_TRIGGER, false);
    }

    public void FinalAttackCalled(){
        finalAttack = true;
        attackInput = false;
        animator.SetBool(ATTACK_TRIGGER, false);
    }
}