using UnityEngine;

public class PlayerAttackController : PlayerAbstract
{
    static readonly int AttackHash = Animator.StringToHash("Attack");
    static readonly int ComboIndexHash = Animator.StringToHash("ComboIndex");
    static readonly int AttackContextHash = Animator.StringToHash("AttackContext");

    [SerializeField] protected AttackContext currentContext;
    [SerializeField] protected bool comboWindowOpen;
    public void RequestAttack()
    {
        if (playerCtrl.PlayerDash.IsDashing) return;

        if (!playerCtrl.PlayerMotor.IsGrounded)
        {

            if (playerCtrl.PlayerAttackState.IsAttacking)
                return;
            RequestAirAttack();
            return;
        }

        RequestGroundAttack();
    }
    void RequestGroundAttack()
    {
        currentContext = AttackContext.Ground;

        if (playerCtrl.PlayerAttackState.IsAttacking)
        {
            if (comboWindowOpen)
                playerCtrl.CharacterCombo.QueueCombo();
            return;
        }

        StartAttack();
    }

    void RequestAirAttack()
    {
        if (playerCtrl.PlayerMotor.VerticalVelocity != 0)
        {
            RequestPlungeAttack();
            return;
        }

        currentContext = AttackContext.Air;

        if (playerCtrl.PlayerAttackState.IsAttacking)
            return;

        StartAttack();
    }
    void RequestPlungeAttack()
    {
        if (playerCtrl.PlayerAttackState.IsAttacking &&
            currentContext == AttackContext.Air)
        {
            playerCtrl.PlayerAttackState.UnlockInput();
        }

        currentContext = AttackContext.Plunge;
        StartAttack();
    }

    void StartAttack()
    {
        if (playerCtrl.PlayerAttackState.IsAttacking) return;
        playerCtrl.PlayerAttackState.LockInput();

        if (currentContext == AttackContext.Ground)
        {
            playerCtrl.CharacterCombo.AdvanceCombo();
        }
        else
        {
            playerCtrl.CharacterCombo.ResetCombo();
            playerCtrl.CharacterCombo.AdvanceCombo();
        }

        var profile = playerCtrl.CharacterCombo.CurrentProfile;

        playerCtrl.PlayerAttackState.SetCurrentProfile(profile);

        playerCtrl.Animator.SetInteger(
            ComboIndexHash,
            playerCtrl.CharacterCombo.CurrentCombo
        );

        playerCtrl.Animator.SetInteger(
            AttackContextHash,
            (int)currentContext
        );

        playerCtrl.Animator.SetTrigger(AttackHash);
    }

    public void OpenComboWindow() => comboWindowOpen = true;
    public void CloseComboWindow()
    {
        comboWindowOpen = false;
        TryConsumeCombo();
    }
    private void TryConsumeCombo()
    {
        if (!playerCtrl.CharacterCombo.HasQueuedCombo) return;
        if (!playerCtrl.CharacterCombo.CanAdvanceCombo()) return;

        playerCtrl.CharacterCombo.ConsumeQueuedCombo();
        StartAttack();
    }
    public void CancelAttackByDash()
    {
        comboWindowOpen = false;
        playerCtrl.CharacterCombo.ResetCombo();
        playerCtrl.PlayerAttackState.UnlockInput();
    }
}
