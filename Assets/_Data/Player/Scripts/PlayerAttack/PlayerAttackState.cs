public class PlayerAttackState : CharacterAttackState
{
    public bool LockJump { get; private set; }

    public void LockJumpWhileAir()
    {
        LockJump = true;
    }

    public void UnlockJump()
    {
        LockJump = false;
    }
}