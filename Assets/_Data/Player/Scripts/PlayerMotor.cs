using UnityEngine;

public class PlayerMotor : PlayerAbstract
{
    Vector3 gravityVelocity;
    public float VerticalVelocity => gravityVelocity.y;
    private bool isGrounded;
    public bool IsGrounded => isGrounded;
    [Header("Gravity")]
    [SerializeField] float gravity = -20f;
    [SerializeField] float jumpForce = 6f;

    protected override void Update()
    {
        HandleGroundCheck();
        HandleJump();
        ApplyGravity();

        playerCtrl.PlayerDash.Tick();
        Vector3 moveVel = Vector3.zero;

        if (playerCtrl.CharacterState.CanMove() &&
            !playerCtrl.PlayerAttackState.LockMovement)
        {
            moveVel = playerCtrl.PlayerMovement.GetVelocity();
        }

        Vector3 velocity =
            moveVel +
            playerCtrl.PlayerDash.GetVelocity() +
            playerCtrl.PlayerAttackMotion.GetVelocity() +
            gravityVelocity;

        playerCtrl.CharacterController.Move(velocity * Time.deltaTime);
        playerCtrl.PlayerAnimation.SetGround(isGrounded);
        playerCtrl.PlayerAnimation.Move();
    }
    void HandleJump()
    {
        if (!isGrounded && playerCtrl.PlayerInput.JumpPressed)
        {
            playerCtrl.PlayerInput.ConsumeJump();
            return;
        }

        if (playerCtrl.PlayerInput.JumpPressed &&
            isGrounded &&
            !playerCtrl.PlayerAttackState.LockJump &&
            !playerCtrl.CharacterState.IsCasting)
        {
            gravityVelocity.y = jumpForce;
            playerCtrl.PlayerAnimation.Jump();
            playerCtrl.PlayerInput.ConsumeJump();
        }
    }

    void HandleGroundCheck()
    {
        isGrounded = playerCtrl.CharacterController.isGrounded;

        if (isGrounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = -2f;
        }
    }

    void ApplyGravity()
    {
        gravityVelocity.y += gravity * Time.deltaTime;
    }
}
