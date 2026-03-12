using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SkillEffect/Buff")]
public class BuffEffect : SkillEffect
{
    public StatType stat;
    public ModifierType modifierType;
    public float value;

    [Header("Timing")]
    public float castTime = 0.8f;
    public float duration = 5f;

    [Header("Effect")]
    public EffectData buffEffect;

    public override void Execute(CharacterCtrl caster, CharacterCtrl target, SkillRuntime skill)
    {
        if (caster == null) return;

        caster.StartCoroutine(ApplyBuff(caster, skill));
    }

    private IEnumerator ApplyBuff(CharacterCtrl caster, SkillRuntime skill)
    {
        if (caster == null) yield break;

        caster.CharacterAttackState.LockInput();

        yield return new WaitForSeconds(castTime);

        CharacterStat statComp = caster.CharacterStat;

        if (statComp == null)
        {
            caster.CharacterAttackState.UnlockInput();
            yield break;
        }

        StatValue targetStat = statComp.GetStat(stat);

        if (targetStat == null)
        {
            caster.CharacterAttackState.UnlockInput();
            yield break;
        }

        float totalDuration = duration + skill.SkillModifier.BonusDuration;

        float finalValue = value;

        if (stat == StatType.Damage)
        {
            if (modifierType == ModifierType.Flat)
                finalValue += skill.SkillModifier.BonusFlatDamage;

            if (modifierType == ModifierType.Percent)
                finalValue += skill.SkillModifier.BonusPercentDamage;
        }
        StatModifier modifier = new StatModifier(stat, modifierType, finalValue);
        modifier.Source = skill;

        targetStat.AddModifier(modifier);

        EffectCtrl activeEffect = null;

        if (buffEffect != null)
        {
            activeEffect = EffectManager.Instance.SpawnEffect(
                buffEffect,
                caster.transform.position,
                caster.transform.rotation,
                caster.transform,
                totalDuration
            );
        }
        else
        {
            Debug.Log("BuffEffect is NULL");
        }

        caster.CharacterAttackState.UnlockInput();

        yield return new WaitForSeconds(totalDuration);

        targetStat.RemoveModifier(modifier);

        Debug.Log($"Buff {stat} ended");
    }
}