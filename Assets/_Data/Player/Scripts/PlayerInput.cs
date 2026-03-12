using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : PlayerAbstract
{
    public Vector2 Move { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool RunHeld { get; private set; }
    public event Action InventoryPressed;
    bool IsLocked => playerCtrl.PlayerLockHandler.IsLocked;
    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (IsLocked)
        {
            Move = Vector2.zero;
            return;
        }

        Move = ctx.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (IsLocked) return;

        if (ctx.performed)
            JumpPressed = true;
    }

    public void ConsumeJump()
    {
        JumpPressed = false;
    }

    public void OnDash(InputAction.CallbackContext ctx)
    {
        if (IsLocked) return;

        if (!ctx.performed) return;

        Vector3 moveDir = playerCtrl.PlayerMovement.GetMoveDirection();
        playerCtrl.PlayerDash.TryDash(moveDir);
    }

    public void OnRun(InputAction.CallbackContext ctx)
    {
        if (IsLocked)
        {
            RunHeld = false;
            return;
        }

        RunHeld = ctx.ReadValue<float>() > 0;
    }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (IsLocked) return;

        if (ctx.performed)
            playerCtrl.PlayerAttackController.RequestAttack();
    }
    public void OnOpenInventory(InputAction.CallbackContext ctx)
    {
        if (ctx.started) InventoryPressed?.Invoke();
    }
    public void OnPickup(InputAction.CallbackContext ctx)
    {
        if (IsLocked) return;

        if (ctx.performed) playerCtrl.PlayerPickup?.TryPickup();
    }
    public void OnScroll(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        if (IsLocked) return;
        if (!UIManager.Instance.PickupUI.IsShowing) return;

        float scrollY = ctx.ReadValue<Vector2>().y;

        if (scrollY > 0)
            UIManager.Instance.PickupUI.ChangeSelection(-1);
        else if (scrollY < 0)
            UIManager.Instance.PickupUI.ChangeSelection(1);
    }
    public void OnSkill1(InputAction.CallbackContext ctx)
    {
        if (IsLocked) return;

        if (ctx.performed) playerCtrl.SkillManager.TryCast(0);
    }

    public void OnSkill2(InputAction.CallbackContext ctx)
    {
        if (IsLocked) return;

        if (ctx.performed) playerCtrl.SkillManager.TryCast(1);
    }
    public void OnSkill3(InputAction.CallbackContext ctx)
    {
        if (IsLocked) return;

        if (ctx.performed) playerCtrl.SkillManager.TryCast(2);
    }
    public void OnSkill4(InputAction.CallbackContext ctx)
    {
        if (IsLocked) return;

        if (ctx.performed) playerCtrl.SkillManager.TryCast(3);
    }
    public void OnSkill5(InputAction.CallbackContext ctx)
    {
        if (IsLocked) return;

        if (ctx.performed) playerCtrl.SkillManager.TryCast(4);
    }
}