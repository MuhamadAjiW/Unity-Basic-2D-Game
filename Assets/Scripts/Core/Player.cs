public class Player : RigidEntity, IMovingEntity
{
    public float walkSpeed = 25;
    public float sprintSpeed = 50;
    public float jumpForce = 600;
    public int jumpCounter = 2;


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
            _ => 0,
        };
    }

    void Update(){
        movement.Jump();
        stance.Execute();
    }

    void FixedUpdate(){
        // Util.printPlayerState(stateController.GetState());
        stateController.UpdateState();
        movement.Move();
    }
}
