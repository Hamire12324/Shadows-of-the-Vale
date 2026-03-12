using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SkillEffect/Heal")]
public class HealEffect : SkillEffect
{
    public float healAmount;
    public float healPercent;

    public override void Execute(CharacterCtrl caster, CharacterCtrl target, SkillRuntime skill)
    {
        var receiver = caster.GetComponentInChildren<DamReceiverBase>();
        if (receiver == null) return;

        var stat = caster.GetComponentInChildren<CharacterStat>();
        if (stat == null) return;

        float maxHP = stat.MaxHP.FinalValue;

        float heal = healAmount;

        heal += maxHP * healPercent;

        heal += skill.SkillModifier.BonusHealFlat;
        heal += maxHP * skill.SkillModifier.BonusHealPercent;

        receiver.Heal(heal);
    }
}