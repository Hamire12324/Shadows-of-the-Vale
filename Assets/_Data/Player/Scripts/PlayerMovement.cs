using UnityEngine;

public class PlayerMovement : PlayerAbstract
{
    [Header("Move")]
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float rotationSpeed = 10f;
    bool canRunAfterDash;

    [Header("Stamina")]
    [SerializeField] float runStaminaCost = 15f;

    bool isRunning;
    public bool IsRunning => isRunning;

    Vector3 moveDir;
    public Vector3 GetVelocity()
    {
        if (playerCtrl.PlayerAttackState.LockMovement)
        {
            isRunning = false;
            moveDir = Vector3.zero;
            return Vector3.zero;
        }

        Vector2 input = playerCtrl.PlayerInput.Move;
        if (input == Vector2.zero)
        {
            isRunning = false;
            moveDir = Vector3.zero;
            canRunAfterDash = false;
            return Vector3.zero;
        }

        HandleRunStamina();

        Vector3 forward = playerCtrl.MainCamera.forward;
        Vector3 right = playerCtrl.MainCamera.right;
        forward.y = right.y = 0;

        moveDir = forward.normalized * input.y +
                  right.normalized * input.x;

        float speed = isRunning ? runSpeed : walkSpeed;

        HandleRotation();

        return moveDir.normalized * speed;
    }
    void HandleRunStamina()
    {
        if (!canRunAfterDash)
        {
            isRunning = false;
            return;
        }

        isRunning = playerCtrl.PlayerStamina.Consume(runStaminaCost * Time.deltaTime);

        if (!isRunning) canRunAfterDash = false;
    }
    void HandleRotation()
    {
        if (moveDir == Vector3.zero) return;
        if (playerCtrl.PlayerAttackState.LockRotation) return;

        playerCtrl.transform.rotation = Quaternion.Slerp(
            playerCtrl.transform.rotation,
            Quaternion.LookRotation(moveDir),
            rotationSpeed * Time.deltaTime
        );
    }
    public Vector3 GetMoveDirection()
    {
        Vector2 input = playerCtrl.PlayerInput.Move;

        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        return (camForward * input.y + camRight * input.x).normalized;
    }
    public float GetAnimatorSpeed()
    {
        if (playerCtrl.PlayerAttackState.LockMovement) return 0f;
        if (playerCtrl.PlayerInput.Move == Vector2.zero) return 0f;
        if (isRunning) return 1f;

        return 0.5f;
    }
    public void EnableRunAfterDash()
    {
        canRunAfterDash = true;
    }
    public void DisableRunAfterDash()
    {
        canRunAfterDash = false;
    }
}
