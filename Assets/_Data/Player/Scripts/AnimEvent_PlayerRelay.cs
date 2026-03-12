using UnityEngine;

public class AnimEvent_PlayerRelay : AnimEvent_BaseRelay
{
    [SerializeField] protected PlayerCtrl playerCtrl;
    [SerializeField] private float lungeSpeed = 5f;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        playerCtrl = GetComponentInParent<PlayerCtrl>();
    }
    public override void EnableWeaponDamage()
    {
        var combo = playerCtrl.CharacterCombo;
        if (combo == null) return;

        var profile = combo.CurrentProfile;
        if (profile == null) return;

        var damageInfo = combo.BuildDamageInfo();

        playerCtrl.WeaponManager.WeaponDamSender.SetDamageInfo(damageInfo);
        playerCtrl.WeaponManager.WeaponDamSender.SetHitEffect(profile.hitEffect);
        playerCtrl.WeaponManager.WeaponDamSender.EnableDamage();
    }
    public override void DisableWeaponDamage()
    {
        playerCtrl.WeaponManager.WeaponDamSender.DisableDamage();
    }

    public void ResetCombo()
    {
        playerCtrl.CharacterCombo.ResetCombo();
    }

    public void OpenComboWindow()
    {
        playerCtrl.PlayerAttackController.OpenComboWindow();
    }

    public void CloseComboWindow()
    {
        playerCtrl.PlayerAttackController.CloseComboWindow();
    }

    public void AttackMoveToTarget(float distance)
    {
        var enemy = playerCtrl.PlayerTargetFinder.FindNearest();

        if (enemy != null)
        {
            playerCtrl.PlayerAttackMotion.AttackMoveToTarget(
                enemy,
                distance,
                lungeSpeed
            );
        }
        else
        {
            playerCtrl.PlayerAttackMotion.AttackMoveForward(
                distance,
                lungeSpeed
            );
        }
    }
}