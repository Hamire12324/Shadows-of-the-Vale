using UnityEngine;

public class PlayerLockHandler : PlayerAbstract
{
    public bool IsLocked { get; private set; }
    public System.Action<bool> OnLockChanged;
    public void LockPlayer()
    {
        IsLocked = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        OnLockChanged?.Invoke(true);
    }

    public void UnlockPlayer()
    {
        IsLocked = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        OnLockChanged?.Invoke(false);
    }
}