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
        return stateController.getState();
    }

    private new void Awake(){
        base.Awake();
        movement = new PlayerMovement(this);
        stateController = new PlayerStateController(this);
    }

    public float getHorizontalForce(){
        if(grounded){
            switch (stateController.getState()){
                case PlayerState.WALKING:
                    currentForce = walkForce;
                    break;
                case PlayerState.SPRINTING:
                    currentForce = sprintForce;
                    break;
                default:
                    currentForce = 0;
                    break;
            }
        } else{
            currentForce =
                     stateController.getState() == PlayerState.STANCE? 0 :
                     currentForce > Constants.PLAYER_JUMP_MINIMUM_SPEED?
                     currentForce : Constants.PLAYER_JUMP_MINIMUM_SPEED;
        }

        return currentForce;
    }

    public float getVerticalForce(){
        return jumpForce;
    }

    public float getMaxSpeed(){
        switch(getPlayerState()){
            case PlayerState.WALKING:
                return walkSpeed;
            case PlayerState.SPRINTING:
                return sprintSpeed;
            default:
                return 0;
        }
    }

    void Update(){
        movement.jump();
    }

    void FixedUpdate(){
        // Util.printPlayerState(stateController.GetState());
        stateController.updateState();
        movement.move();
    }
}
