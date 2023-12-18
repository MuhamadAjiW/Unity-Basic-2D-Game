public class Player : RigidEntity, IMovingEntity
{
    public float walkSpeed = 10;
    public float sprintSpeed = 25;
    public float jumpForce = 600;
    public int jumpCounter = 2;

    private float snapshotSpeed = 25;

    private PlayerMovement movement;
    private PlayerStance stance;
    private PlayerStateController stateController;

    public Player(bool grounded) : base(grounded){}

    public PlayerState GetPlayerState(){
        return stateController.GetState();
    }

    private new void Awake(){
        base.Awake();
        movement = new PlayerMovement(this);
        stateController = new PlayerStateController(this);
        stance = new PlayerStance(this);
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

    public void SetSnapshotSpeed(float speed){
        snapshotSpeed = speed;
    }

    void Update(){
        movement.Jump();
        stance.Execute();
    }

    void FixedUpdate(){
        Util.PrintPlayerState(stateController.GetState());
        stateController.UpdateState();
        movement.Move();
    }
}
