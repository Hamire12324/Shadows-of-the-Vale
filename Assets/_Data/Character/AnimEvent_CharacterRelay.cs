public abstract class AnimEvent_BaseRelay : CharacterAbstract
{
    public virtual void EnableWeaponDamage()
    {
        characterCtrl.WeaponManager.EnableDamage();
    }

    public virtual void DisableWeaponDamage()
    {
        characterCtrl.WeaponManager.DisableDamage();
    }
    public void AttackStart()
    {
        characterCtrl.CharacterAttackState.LockInput();
    }
    public void AttackEnd()
    {
        characterCtrl.CharacterAttackState.UnlockInput();
    }

    public void SpawnCurrentAttackEffect()
    {
        var profile = characterCtrl.CharacterAttackState.CurrentProfile;
        if (profile?.slashEffect == null) return;

        characterCtrl.CharacterAttackMotion.SpawnEffect(profile.slashEffect);
    }
}