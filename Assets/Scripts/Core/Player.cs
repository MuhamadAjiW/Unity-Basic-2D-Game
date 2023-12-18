public class Player : RigidEntity, IMovingEntity
{
    private float currentForce = 0;
    public float walkForce = 100;
    public float walkSpeed = 25;
    public float sprintForce = 150;
    public float sprintSpeed = 50;
    public float jumpForce = 600;
    public int jumpCounter = 2;


    private PlayerMovement movement;
    private PlayerStateController stateController;

    public Player(bool grounded) : base(grounded){}

    public PlayerState getPlayerState(){
        return stateController.GetState();
    }

    private new void Awake(){
        base.Awake();
        movement = new PlayerMovement(this);
        stateController = new PlayerStateController(this);
    }

    public float GetHorizontalForce(){
        if(IsGrounded()){
            currentForce = stateController.GetState() switch
            {
                PlayerState.WALKING => walkForce,
                PlayerState.SPRINTING => sprintForce,
                _ => 0,
            };
        } else{
            currentForce =
                     stateController.GetState() == PlayerState.STANCE? 0 :
                     currentForce > Constants.PLAYER_JUMP_MINIMUM_SPEED?
                     currentForce : Constants.PLAYER_JUMP_MINIMUM_SPEED;
        }

        return currentForce;
    }

    public float GetVerticalForce(){
        return jumpForce;
    }

    public float GetMaxSpeed(){
        return getPlayerState() switch
        {
            PlayerState.WALKING => walkSpeed,
            PlayerState.SPRINTING => sprintSpeed,
            _ => 0,
        };
    }

    void Update(){
        movement.Jump();
    }

    void FixedUpdate(){
        // Util.printPlayerState(stateController.GetState());
        stateController.UpdateState();
        movement.Move();
    }
}
