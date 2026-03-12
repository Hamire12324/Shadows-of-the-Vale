public class CharacterAttackState : CharacterAbstract
{
    public bool IsAttacking { get; protected set; }
    public bool LockMovement { get; protected set; }
    public bool LockRotation { get; protected set; }

    protected AttackProfile currentProfile;
    public AttackProfile CurrentProfile => currentProfile;

    public virtual void LockInput()
    {
        IsAttacking = true;
        LockMovement = true;
        LockRotation = true;
    }

    public virtual void UnlockInput()
    {
        IsAttacking = false;
        LockMovement = false;
        LockRotation = false;
    }

    public void SetCurrentProfile(AttackProfile profile)
    {
        currentProfile = profile;
    }
}