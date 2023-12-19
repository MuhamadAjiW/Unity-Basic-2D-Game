using System;
using UnityEngine;

public class Player : RigidObject, IMovingEntity, IDamageableEntity{
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
    public event Action OnDamaged;


    public float Health {
        get { return health; }
        set { health = value; }
    }
    public bool Dead {
        get { return health <= 0; }
    }

    public float SnapshotSpeed{
        set { snapshotSpeed = value; }
    }

    public int JumpLimit{
        get { return jumpLimit; }
    }

    public int State{
        get { return stateController.State; }
    }

    private new void Awake(){
        base.Awake();
        movement = new PlayerMovement(this);
        stateController = new PlayerStateController(this);
        stance = new PlayerStance(this, ignoreWhileDashing);
        OnDamaged += Damaged;
        OnDeath += Death;
        stateController.OnDamageDelayOver += DamageCleared;
    }

    private void Death(){
        Debug.Log("Player is dead");
    }

    private void Damaged(){
        SpriteRenderer.color = Color.red;

        stateController.OnDamaged();
        Debug.Log("Player is damaged");
    }

    private void DamageCleared(){
        if(!Dead){
            SpriteRenderer.color = Color.white;
            Debug.Log("Player is no longer damaged");
        }
    }

    public float GetJumpForce(){
        return jumpForce;
    }

    public float GetMaxSpeed(){
        return State switch
        {
            PlayerState.WALKING => walkSpeed,
            PlayerState.SPRINTING => sprintSpeed,
            PlayerState.JUMPING => snapshotSpeed,
            PlayerState.FALLING => snapshotSpeed,
            _ => 0,
        };
    }

    public float InflictDamage(float damage){
        if(!Dead && !stateController.Damaged){
            OnDamaged?.Invoke();

            Debug.Log(string.Format("Remaining health: {0}", health));
            health -= damage;
            Debug.Log(string.Format("Remaining health: {0}", health));
            if(Dead) OnDeath?.Invoke();
        }
        return health;
    }

    void Update(){
        base.refresh();
        movement.Jump();
        stance.Execute();
    }

    void FixedUpdate(){
        // Util.PrintPlayerState(stateController);
        stateController.UpdateState();
        movement.Move();
    }

}
