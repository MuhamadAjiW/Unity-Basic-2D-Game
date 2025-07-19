using System.Collections;
using UnityEngine;
using static ConfigurationManager;

public class PlayerMovementController
{
    private Player player;
    private int jumpCounter;
    private bool jumpDelayOver = true;

    public PlayerMovementController(Player player)
    {
        this.player = player;
        jumpCounter = player.JumpLimit;
    }

    public void Move()
    {
        float keyPress = player.State == PlayerState.STANCE ? 0 : Input.GetAxisRaw("Horizontal");
        Vector2 velocity = new(player.Rigidbody.velocity.x, player.Rigidbody.velocity.y);

        Vector2 dampVelocity = Vector2.zero;
        if (keyPress == 0)
        {
            velocity.x = 0;
        }
        else
        {
            float maxSpeed = player.MaxSpeed;
            velocity.x = keyPress * maxSpeed;
        }

        player.Rigidbody.velocity = Vector2.SmoothDamp(player.Rigidbody.velocity, velocity, ref dampVelocity, Instance.playerConfig.MOVEMENT_SMOOTHING);
        if (player.Rigidbody.velocity.y < 0)
        {
            player.Rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (Instance.playerConfig.FALL_SPEED_MULTIPLIER - 1) * Time.deltaTime;
        }
        else if (player.Rigidbody.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            player.Rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (Instance.playerConfig.JUMP_LOW_MULTIPLIER - 1) * Time.deltaTime;
        }
    }

    public void Jump()
    {
        if (Input.GetButtonDown("Jump") && player.State != PlayerState.STANCE && jumpCounter > 0 && jumpDelayOver)
        {
            float snapshotSpeed = Mathf.Abs(player.Rigidbody.velocity.x * 1.3f);
            player.SnapshotSpeed = Mathf.Abs(snapshotSpeed > Instance.playerConfig.JUMP_MINIMUM_SPEED ? snapshotSpeed : Instance.playerConfig.JUMP_MINIMUM_SPEED);

            Vector2 force = new Vector2(0, player.JumpForce);

            player.Rigidbody.AddForce(force, ForceMode2D.Impulse);

            jumpCounter -= 1;

            jumpDelayOver = false;
            player.StartCoroutine(DelayJump());
        }
        else if (player.Grounded && jumpDelayOver)
        {
            jumpCounter = player.JumpLimit;
        }
    }

    private IEnumerator DelayJump()
    {
        for (int i = 0; i < Instance.playerConfig.JUMP_DELAY; i++)
        {
            yield return new WaitForFixedUpdate();
        }
        jumpDelayOver = true;
    }
}
