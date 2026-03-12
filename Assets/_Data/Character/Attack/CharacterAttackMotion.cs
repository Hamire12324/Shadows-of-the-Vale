using UnityEngine;
using System.Collections;

public class CharacterAttackMotion : CharacterAbstract
{
    protected Vector3 attackVelocity;
    protected Transform target;

    public void AttackMoveForward(float distance, float speed)
    {
        StartCoroutine(Lunge(distance, speed));
    }

    public void AttackMoveToTarget(Transform enemy, float distance, float speed)
    {
        target = enemy;
        StartCoroutine(LungeToTarget(distance, speed));
    }

    IEnumerator Lunge(float distance, float speed)
    {
        float moved = 0f;

        while (moved < distance && characterCtrl.CharacterAttackState.IsAttacking)
        {
            float step = speed * Time.deltaTime;

            attackVelocity = transform.forward * speed;

            moved += step;

            yield return null;
        }

        attackVelocity = Vector3.zero;
    }

    IEnumerator LungeToTarget(float distance, float speed)
    {
        float moved = 0f;

        while (moved < distance &&
               characterCtrl.CharacterAttackState.IsAttacking &&
               target != null)
        {
            Vector3 dir = target.position - transform.position;
            dir.y = 0;

            float distToTarget = dir.magnitude;

            if (distToTarget < 1.2f)
                break;

            dir.Normalize();

            transform.parent.forward = dir;

            float step = speed * Time.deltaTime;

            attackVelocity = dir * speed;

            moved += step;

            yield return null;
        }

        attackVelocity = Vector3.zero;
        target = null;
    }

    public Vector3 GetVelocity()
    {
        if (!characterCtrl.CharacterAttackState.IsAttacking)
            return Vector3.zero;

        return attackVelocity;
    }

    public void Stop()
    {
        attackVelocity = Vector3.zero;
    }

    public void SpawnEffect(EffectData data)
    {
        if (data == null || data.prefab == null) return;

        Vector3 pos = transform.position
                    + transform.forward * data.offset.z
                    + transform.right * data.offset.x
                    + Vector3.up * data.offset.y;

        Quaternion baseRot = Quaternion.LookRotation(transform.forward);
        Quaternion finalRot = baseRot * Quaternion.Euler(data.rotationOffset);

        EffectManager.Instance.SpawnEffect(data, pos, finalRot);
    }
}