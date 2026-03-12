using System;
using UnityEngine;
public class SkillRuntime
{
    private SkillData skillData;
    public SkillData SkillData => skillData;
    private SkillManager skillManager;
    public SkillManager SkillManager => skillManager;
    private SkillCooldown skillCooldown;
    public SkillCooldown SkillCooldown => skillCooldown;
    private SkillResourceCost skillResourceCost;
    public SkillResourceCost SkillResourceCost => skillResourceCost;
    private SkillModifier skillModifier;
    public SkillModifier SkillModifier => skillModifier;
    public event Action OnRuntimeChanged;
    public SkillRuntime(SkillData skillData, SkillManager skillManager)
    {
        this.skillData = skillData;
        this.skillManager = skillManager;

        skillCooldown = new SkillCooldown(skillData.cooldown);
        skillResourceCost = new SkillResourceCost(skillData.resourceType, skillData.cost);

        skillModifier = new SkillModifier();
    }
    public bool CanCast()
    {
        PlayerCtrl caster = skillManager.PlayerCtrl;

        if (caster.CharacterState.IsCasting)
            return false;

        if (skillCooldown.IsOnCooldown())
            return false;

        if (!skillResourceCost.CanPay(skillManager, this))
            return false;

        return true;
    }

    public void Cast()
    {
        if (!CanCast()) return;

        skillResourceCost.Pay(skillManager, this);

        skillCooldown.Trigger(GetFinalCooldown());

        PlayerCtrl caster = skillManager.PlayerCtrl;

        if (caster != null && caster.Animator != null)
        {
            caster.Animator.SetInteger("SkillIndex", skillData.animationIndex);
            caster.Animator.SetTrigger("CastSkill");
        }

        skillManager.SkillExecutionSystem.Cast(this);
    }
    public float GetFinalCooldown()
    {
        float baseCooldown = skillData.cooldown;

        float reduction = skillModifier.BonusCooldownReduction;

        float finalCooldown = baseCooldown - reduction;

        if (finalCooldown < 0.05f)
            finalCooldown = 0.05f;

        return finalCooldown;
    }
    public void NotifyRuntimeChanged()
    {
        OnRuntimeChanged?.Invoke();
    }
    public float CooldownNormalized => skillCooldown.GetCooldownNormalized();
}