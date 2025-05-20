using UnityEngine;

public class ShootState : PlayerBaseState
{
    // Store reference to the state machine
    // No factory needed based on PlayerStateMachine.cs structure

    public ShootState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    public GameObject projectilePrefab; // Prefab for the projectile

    public override void Enter()
    {
        // Logic when entering the shoot state (e.g., play animation, aim)
        Debug.Log("Player entered Shoot State");
        // Ctx.Animator.SetBool("IsShooting", true); // Example animation trigger
    }

    public override void Tick(float deltaTime)
    {
        // Logic during the shoot state (e.g., handle firing cooldown, check ammo)

        // Check for transitions out of the shoot state
        // Check for transitions out of the shoot state
        CheckSwitchStates(); // We'll keep this helper method for clarity

        /*if (Input.GetMouseButtonDown(0)) // Example input for shooting
        {
            FireProjectile();
        }
        */
    }

    public override void Exit()
    {
        // Logic when exiting the shoot state (e.g., stop animation)
        Debug.Log("Player exited Shoot State");
        // Ctx.Animator.SetBool("IsShooting", false); // Example animation reset
    }

    /*private void FireProjectile()
    {
        // Logic to fire a projectile
         // Instantiate the projectile at the shoot point's position and rotation
        if (projectilePrefab != null && shootPoint != null)
        {
            // Instantiate the projectile at the shoot point's position and rotation
            GameObject projectile = GameObject.Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

            // Optionally, add force to the projectile if it has a Rigidbody
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //rb.AddForce(shootPoint.forward * 10f, ForceMode.Impulse); // Adjust force as needed
            }

            Debug.Log("Projectile fired!");
        }
        else
        {
            Debug.LogWarning("ProjectilePrefab or ShootPoint is not assigned!");
        }
            
    }
    */

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