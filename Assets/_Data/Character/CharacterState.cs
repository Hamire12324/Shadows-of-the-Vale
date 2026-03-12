using UnityEngine;

public class CharacterState : MonoBehaviour
{
    public bool IsDead { get; protected set; }
    public bool IsStunned { get; protected set; }
    public bool IsInvincible { get; protected set; }
    public bool IsMovementLocked { get; protected set; }
    public bool IsCasting { get; protected set; }

    public virtual void Die()
    {
        IsDead = true;
        LockMovement();
    }

    public virtual void Revive()
    {
        IsDead = false;
        UnlockMovement();
    }

    public virtual void LockMovement()
    {
        IsMovementLocked = true;
    }

    public virtual void UnlockMovement()
    {
        IsMovementLocked = false;
    }

    public virtual bool CanMove()
    {
        return !IsDead && !IsStunned && !IsMovementLocked;
    }
    public virtual bool CanJump()
    {
        return !IsDead && !IsStunned && !IsCasting;
    }
    public virtual void SetCasting(bool value)
    {
        IsCasting = value;
    }
}