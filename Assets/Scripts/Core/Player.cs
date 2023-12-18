using System;
using UnityEngine;

public class Player : RigidEntity, IMovingEntity, IDamageableEntity{
    [SerializeField] private float health = 100;
    [SerializeField] private float walkSpeed = 10;
    [SerializeField] private float sprintSpeed = 25;
    [SerializeField] private int jumpLimit = 2;
    [SerializeField] private float jumpForce = 600;
    [SerializeField] private LayerMask ignoreWhileDashing;
    private float snapshotSpeed = 25;

    private PlayerMovement movement;
    private PlayerStance stance;
    private PlayerStateController stateController;

    public event Action OnDeath;

    public Player(bool grounded) : base(grounded){}

    public PlayerState GetPlayerState(){
        return stateController.GetState();
    }

    private new void Awake(){
        base.Awake();
        movement = new PlayerMovement(this);
        stateController = new PlayerStateController(this);
        stance = new PlayerStance(this, ignoreWhileDashing);
        OnDeath += Death;
    }

    private void Death(){
        Debug.Log("Player is dead");
    }

    public float GetJumpForce(){
        return jumpForce;
    }

    public float GetMaxSpeed(){
        return GetPlayerState() switch
        {
            PlayerState.WALKING => walkSpeed,
            PlayerState.SPRINTING => sprintSpeed,
            PlayerState.JUMPING => snapshotSpeed,
            PlayerState.FALLING => snapshotSpeed,
            _ => 0,
        };
    }
    public bool IsDead(){
        return health <= 0;
    }

    public float GetHealth(){
        return health;
    }

    public float InflictDamage(float damage){
        Debug.Log(string.Format("Remaining health: {0}", health));
        health -= damage;
        Debug.Log(string.Format("Remaining health: {0}", health));
        if(IsDead()) OnDeath?.Invoke();
        return health;
    }

    public void SetSnapshotSpeed(float speed){
        snapshotSpeed = speed;
    }

    public int GetJumpLimit(){
        return jumpLimit;
    }
    

    void Update(){
        movement.Jump();
        stance.Execute();
    }

    void FixedUpdate(){
        // Util.PrintPlayerState(stateController.GetState());
        stateController.UpdateState();
        movement.Move();
    }

}
