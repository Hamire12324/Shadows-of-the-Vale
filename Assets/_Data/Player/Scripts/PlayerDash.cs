using UnityEngine;

public class PlayerDash : PlayerAbstract
{
    [Header("Dash Settings")]
    [SerializeField] float dashSpeed = 20f;
    [SerializeField] float dashDuration = 0.25f;
    [SerializeField] float dashCooldown = 0.5f;
    [SerializeField] float staminaCost = 25f;

    float dashTimer;
    float nextDashTime;
    Vector3 dashDirection;
    bool isDashing;

    public bool IsDashing => isDashing;

    Vector3 dashVelocity;
    public void TryDash(Vector3 moveDir)
    {
        if (isDashing) return;
        if (Time.time < nextDashTime) return;

        if (playerCtrl.CharacterState.IsCasting)
        {
            playerCtrl.SkillManager.SkillRuntimeSystem.CancelCasting();
        }

        if (!playerCtrl.PlayerStamina.Consume(staminaCost)) return;

        if (playerCtrl.PlayerAttackState.IsAttacking)
        {
            playerCtrl.PlayerAttackController.CancelAttackByDash();
        }

        isDashing = true;
        dashTimer = dashDuration;

        playerCtrl.PlayerAttackState.LockInput();

        dashDirection = moveDir != Vector3.zero
            ? moveDir.normalized
            : playerCtrl.transform.forward;

        playerCtrl.PlayerAnimation.ForcePlayDash();
        playerCtrl.DamReceiverBase.SetInvincible(true);
    }
    public void Tick()
    {
        if (!isDashing)
        {
            dashVelocity = Vector3.zero;
            return;
        }

        dashVelocity = dashDirection * dashSpeed;

        dashTimer -= Time.deltaTime;
        if (dashTimer <= 0f)
            EndDash();
    }
    protected virtual void EndDash()
    {
        isDashing = false;
        nextDashTime = Time.time + dashCooldown;
        dashVelocity = Vector3.zero;

        playerCtrl.PlayerAttackState.UnlockInput();
        playerCtrl.PlayerMovement.EnableRunAfterDash();
        playerCtrl.DamReceiverBase.SetInvincible(false);
    }
    public Vector3 GetVelocity() => dashVelocity;
}
