using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DamageEffectExecutor
{
    static Dictionary<CharacterCtrl, EffectCtrl> activeEffects = new();

    static Collider[] hitBuffer = new Collider[20];

    static HashSet<CharacterCtrl> hitTargets = new();
    public static void Execute(DamageEffect data, CharacterCtrl caster, SkillRuntime skill)
    {
        if (caster == null) return;

        caster.CharacterState.SetCasting(true);

        Coroutine routine = caster.StartCoroutine(ExecuteRoutine(data, caster, skill));

        caster.SkillManager.SkillRuntimeSystem.RegisterCastingRoutine(routine);
        caster.SkillManager.SkillRuntimeSystem.RegisterCastingSkill(skill);
    }

    static IEnumerator ExecuteRoutine(DamageEffect data, CharacterCtrl caster, SkillRuntime skill)
    {
        hitTargets.Clear();

        yield return new WaitForSeconds(data.delayBeforeImpact);

        if (!caster.CharacterState.IsCasting)
            yield break;

        SpawnSlashEffect(data, caster, skill);

        if (skill.SkillData.castType == SkillCastType.Duration)
            yield return DamageOverTime(data, caster, skill);
        else
            DoDamage(data, caster, skill);

        caster.CharacterState.SetCasting(false);
    }

    static void SpawnSlashEffect(DamageEffect data, CharacterCtrl caster, SkillRuntime skill)
    {

        if (data.slashEffect == null) return;

        Vector3 pos =
            caster.transform.position +
            caster.transform.TransformDirection(data.slashEffect.offset);

        Quaternion rot =
            caster.transform.rotation *
            Quaternion.Euler(data.slashEffect.rotationOffset);

        var effect = EffectManager.Instance.SpawnEffect(
            data.slashEffect,
            pos,
            rot,
            caster.transform,
            data.duration + skill.SkillModifier.BonusDuration
        );

        activeEffects[caster] = effect;
    }

    static IEnumerator DamageOverTime(DamageEffect data, CharacterCtrl caster, SkillRuntime skill)
    {
        caster.Animator.SetBool("IsCasting", true);

        Debug.Log("DamageOverTime");
        float elapsed = 0;
        float totalDuration = data.duration + skill.SkillModifier.BonusDuration;

        while (elapsed < totalDuration)
        {
            hitTargets.Clear();
            if (!caster.CharacterState.IsCasting)
                yield break;

            DoDamage(data, caster, skill);

            yield return new WaitForSeconds(data.tickInterval);

            elapsed += data.tickInterval;
        }

        caster.Animator.SetBool("IsCasting", false);
    }

    static void DoDamage(DamageEffect data, CharacterCtrl caster, SkillRuntime skill)
    {
        int count = Physics.OverlapSphereNonAlloc(
            caster.transform.position,
            data.radius,
            hitBuffer
        );

        for (int i = 0; i < count; i++)
        {
            Collider hit = hitBuffer[i];

            var receiver = hit.GetComponentInChildren<DamReceiverBase>();
            if (receiver == null) continue;

            CharacterCtrl target = hit.GetComponentInParent<CharacterCtrl>();
            if (target == null) continue;

            if (hitTargets.Contains(target))
                continue;

            hitTargets.Add(target);

            if (!FactionManager.CanAttack(caster.faction, receiver.faction))
                continue;

            if (!IsInsideAngle(caster, target, data.attackAngle))
                continue;

            float damage = CalculateDamage(data, caster, skill);

            receiver.ReceiveDamage(damage);

            ApplyExtraEffects(caster, target, skill);

            SpawnHitEffect(data, caster, hit);
        }
    }

    static bool IsInsideAngle(CharacterCtrl caster, CharacterCtrl target, float angle)
    {
        Vector3 dir = (target.transform.position - caster.transform.position).normalized;

        float a = Vector3.Angle(caster.transform.forward, dir);

        return a <= angle * 0.5f;
    }

    static float CalculateDamage(DamageEffect data, CharacterCtrl caster, SkillRuntime skill)
    {
        float baseDamage = caster.CharacterStat.Damage.FinalValue;

        float finalDamage =
            baseDamage * (data.multiplier + skill.SkillModifier.BonusSkillDamagePercent)
            + skill.SkillModifier.BonusSkillDamage;

        if (!data.canCrit)
            return finalDamage;

        float critChance =
            caster.CharacterStat.CritChance.FinalValue +
            skill.SkillModifier.BonusCritChance;
        float critDamage =
            caster.CharacterStat.CritDame.FinalValue +
            skill.SkillModifier.BonusCritDamage;

        if (Random.value <= critChance)
            finalDamage *= critDamage;

        return finalDamage;
    }

    static void ApplyExtraEffects(CharacterCtrl caster, CharacterCtrl target, SkillRuntime skill)
    {
        foreach (var effect in skill.SkillModifier.ExtraEffects)
            effect.Execute(caster, target, skill);
    }

    static void SpawnHitEffect(DamageEffect data, CharacterCtrl caster, Collider hit)
    {
        if (data.hitEffect == null) return;

        Vector3 pos = hit.ClosestPoint(caster.transform.position);

        EffectManager.Instance.SpawnEffect(
            data.hitEffect,
            pos,
            Quaternion.identity
        );
    }

    public static void CancelEffect(CharacterCtrl caster)
    {
        if (!activeEffects.TryGetValue(caster, out var effect))
            return;

        effect.Despawn.DoDespawn();
        activeEffects.Remove(caster);
    }
}