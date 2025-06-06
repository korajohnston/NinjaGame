using UnityEngine;

public class ShootState : PlayerBaseState
{
    // Store reference to the state machine
    // No factory needed based on PlayerStateMachine.cs structure

    public ShootState(PlayerStateMachine stateMachine) : base(stateMachine)
    { 
        if (stateMachine.ShootPoint == null)
    {
        Debug.LogError("ShootPoint is null! Please ensure it is assigned.");
    }
    else
    {
        stateMachine.ShootPoint = GameObject.FindWithTag("ShootPoint")?.transform;
    }
    }

    public PlayerStateMachine stateMachine; // Reference to the PlayerStateMachine script

    public override void Enter()
    {
        // Logic when entering the shoot state (e.g., play animation, aim)
        Debug.Log("Player entered Shoot State");
        //shootPoint = GameObject.FindWithTag("ShootPoint")?.transform;
        // Ctx.Animator.SetBool("IsShooting", true); // Example animation trigger
    }


    public override void Tick(float deltaTime)
    {
        // Logic during the shoot state (e.g., handle firing cooldown, check ammo)

        if (stateMachine.ProjectilePrefab != null && stateMachine.ShootPoint != null)
        {
            // Instantiate the projectile at the shoot point's position and rotation
            GameObject projectile = GameObject.Instantiate(stateMachine.ProjectilePrefab, stateMachine.ShootPoint.position, stateMachine.ShootPoint.rotation);

            // Optionally, add force to the projectile if it has a Rigidbody
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(stateMachine.ShootPoint.forward * 10f, ForceMode.Impulse); // Adjust force as needed
            }

            Debug.Log("Projectile fired!");
        }
        else
        {
            Debug.LogWarning("ProjectilePrefab or ShootPoint is not assigned!");
        }

        CheckSwitchStates();

    }

    public override void Exit()
    {
        // Logic when exiting the shoot state (e.g., stop animation)
        Debug.Log("Player exited Shoot State");
        // Ctx.Animator.SetBool("IsShooting", false); // Example animation reset
    }
      
    // Helper method for transition checks (called from Tick)
    private void CheckSwitchStates()
    {
        // If shoot button is released, transition to appropriate state
        if (!stateMachine.InputReader.IsShootPressed())
        {
            // Allow jump out of shoot if jump pressed
            if (stateMachine.InputReader.IsJumpPressed() && stateMachine.JumpsRemaining > 0)
            {
                stateMachine.SwitchState(stateMachine.JumpState);
                return;
            }

            if (stateMachine.IsGrounded())
            {
                if (stateMachine.InputReader.IsCrouchHeld())
                {
                    stateMachine.SwitchState(stateMachine.CrouchState);
                }
                else
                {
                    Vector2 moveInput = stateMachine.InputReader.GetMovementInput();
                    if (moveInput == Vector2.zero)
                        stateMachine.SwitchState(stateMachine.IdleState);
                    else if (stateMachine.InputReader.IsRunPressed())
                        stateMachine.SwitchState(stateMachine.RunState);
                    else
                        stateMachine.SwitchState(stateMachine.WalkState);
                }
            }
            else
            {
                // Airborne: check for wall cling or fall
                if (stateMachine.IsTouchingWall() && stateMachine.RB.linearVelocity.y <= 0)
                {
                    stateMachine.SwitchState(stateMachine.WallClingState);
                }
                else
                {
                    stateMachine.SwitchState(stateMachine.FallState);
                }
            }
        }
    }

    // No InitializeSubState in the base class shown in PlayerStateMachine.cs
}