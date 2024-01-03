using System;
using UnityEngine;

public class Player : DamageableObject, IMovingEntity, IStatefulEntity{
    [SerializeField] private float walkSpeed = 10;
    [SerializeField] private float sprintSpeed = 25;
    [SerializeField] private int jumpLimit = 2;
    [SerializeField] private float jumpForce = 600;
    [SerializeField] private LayerMask ignoreWhileDashing;
    private float snapshotSpeed = 25;

    private PlayerAnimationController animationController;
    private PlayerAttackController attackController;
    private PlayerMovementController movementController;
    private PlayerStanceController stanceController;
    private PlayerStateController stateController;
    private WeaponObject weapon;
    public override bool Damageable => !Dead && !stateController.Damaged;

    public int State => stateController.State;
    public EntityStateController StateController => stateController;
    public Action AddOnStateChange {
        set { stateController.OnStateChange += value; }
    }

    public float SnapshotSpeed{
        set { snapshotSpeed = value; }
    }

    public int JumpLimit => jumpLimit;
    public float JumpForce => jumpForce;
    public WeaponObject Weapon => weapon;

    public float MaxSpeed => State switch{
        PlayerState.WALKING => walkSpeed,
        PlayerState.SPRINTING => sprintSpeed,
        PlayerState.JUMPING => snapshotSpeed,
        PlayerState.FALLING => snapshotSpeed,
        _ => 0,
    };

    private new void Awake(){
        base.Awake();
        weapon = GetComponentInChildren<WeaponObject>();
        Health *= PlayerConfig.GLOBAL_HEALTH_MULTIPLIER;

        stateController = new PlayerStateController(this);
        animationController = new PlayerAnimationController(this);
        attackController = new PlayerAttackController(this);
        movementController = new PlayerMovementController(this);
        stanceController = new PlayerStanceController(this, ignoreWhileDashing);
    }

    public override float InflictDamage(float damage){
        SpriteRenderer.color = Color.red;
        base.InflictDamage(damage);

        return Health;
    }

    new void Update(){
        base.Update();
        Refresh();
        movementController.Jump();
        stanceController.Execute();
        attackController.Execute();
    }

    new void FixedUpdate(){
        base.FixedUpdate();
        // Util.PrintPlayerState(stateController);
        stateController.UpdateState();
        movementController.Move();
    }

}
