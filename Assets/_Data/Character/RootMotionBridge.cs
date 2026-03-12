using UnityEngine;

public class RootMotionBridge : PlayerAbstract
{
    private bool doImpulse;
    private float impulseForce = 3f;

    public void TriggerAttackImpulse()
    {
        doImpulse = true;
    }

    void OnAnimatorMove()
    {
        if (!playerCtrl.PlayerAttackState.IsAttacking) return;

        Vector3 dir;

        Vector3 animDelta = playerCtrl.Animator.deltaPosition;

        if (animDelta.sqrMagnitude > 0.0001f)
            dir = animDelta.normalized;
        else
            dir = playerCtrl.transform.forward;

        dir.y = 0f;

        float attackMoveSpeed = 4f;

        Vector3 move = dir * attackMoveSpeed * Time.deltaTime;

        if (doImpulse)
        {
            move += dir * impulseForce;
            doImpulse = false;
        }

        playerCtrl.CharacterController.Move(move);
    }
}
