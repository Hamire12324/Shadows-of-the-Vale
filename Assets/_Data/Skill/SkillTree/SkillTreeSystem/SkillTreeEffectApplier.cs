using UnityEngine;

public class SkillTreeEffectApplier
{
    private CharacterCtrl character;
    private SkillRuntimeSystem skillRuntimeSystem;

    public SkillTreeEffectApplier(CharacterCtrl character, SkillRuntimeSystem runtimeSystem)
    {
        this.character = character;
        this.skillRuntimeSystem = runtimeSystem;
    }

    public void ApplyNodeEffects(SkillNodeRuntime node)
    {
        ApplySkillModifiers(node);
        ApplyStatModifiers(node);
        ApplyExtraEffects(node.Data);
    }
    public void ApplySkillModifiers(SkillNodeRuntime node)
    {
        if (node.Data.skillData == null) return;

        var runtime = skillRuntimeSystem.GetSkillByIndex(node.Data.skillData);
        if (runtime == null) return;

        foreach (var modifier in node.Data.modifiers)
        {
            if (modifier == null) continue;
            if (modifier.values == null || modifier.values.Count == 0) continue;

            int index = Mathf.Clamp(node.Level - 1, 0, modifier.values.Count - 1);
            float value = modifier.values[index];

            switch (modifier.type)
            {
                case SkillModifierType.skillDamage:
                    runtime.SkillModifier.AddDamage(value);
                    break;

                case SkillModifierType.skillDamagePercent:
                    runtime.SkillModifier.AddDamagePercent(value);
                    break;

                case SkillModifierType.buffFlatDamage:
                    runtime.SkillModifier.AddBuffFlatDamage(value);
                    break;

                case SkillModifierType.buffPercentDamage:
                    runtime.SkillModifier.AddBuffPercentDamage(value);
                    break;

                case SkillModifierType.Duration:
                    runtime.SkillModifier.AddDuration(value);
                    break;

                case SkillModifierType.CritChance:
                    runtime.SkillModifier.AddCritChance(value);
                    break;

                case SkillModifierType.CritDame:
                    runtime.SkillModifier.AddCritDamage(value);
                    break;

                case SkillModifierType.CooldownReduction:
                    runtime.SkillModifier.AddCooldownReduction(value);
                    break;

                case SkillModifierType.Cost:
                    runtime.SkillModifier.AddCost(value);
                    break;
                case SkillModifierType.StunDuration:
                    runtime.SkillModifier.AddStunDuration(value);
                    break;
            }
        }
    }
    public void ApplyStatModifiers(SkillNodeRuntime node)
    {
        var statComp = character.CharacterStat;
        if (statComp == null) return;

        foreach (var stat in node.Data.statModifiers)
        {
            if (stat == null) continue;
            if (stat.values == null || stat.values.Count == 0) continue;

            var target = statComp.GetStat(stat.statType);
            if (target == null) continue;

            target.RemoveModifierFromSource(node);

            int index = Mathf.Clamp(node.Level - 1, 0, stat.values.Count - 1);
            float value = stat.values[index];

            StatModifier modifier =
                new StatModifier(stat.statType, stat.modifierType, value);

            modifier.Source = node;

            target.AddModifier(modifier);
        }
    }
    public void ApplyExtraEffects(SkillNodeData nodeData)
    {
        if (nodeData.skillData == null) return;

        var runtime = skillRuntimeSystem.GetSkillByIndex(nodeData.skillData);
        if (runtime == null) return;

        if (nodeData.upgradeEffects == null || nodeData.upgradeEffects.Count == 0) return;

        foreach (var effect in nodeData.upgradeEffects)
        {
            if (effect == null) continue;

            runtime.SkillModifier.AddEffect(effect);
        }
    }
}