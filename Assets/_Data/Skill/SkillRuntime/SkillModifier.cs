using System.Collections.Generic;
using UnityEngine;
public class SkillModifier
{
    private float bonusSkillDamage;
    private float bonusSkillDamagePercent;

    private float bonusBuffFlatDamage;
    private float bonusBuffPercentDamage;

    private float bonusDuration;
    private float bonusStunDuration;

    private float bonusCritChance;
    private float bonusCritDamage;
    private float bonusCost;
    private float bonusCooldownReduction;
    private float bonusHealFlat;
    private float bonusHealPercent;
    private List<SkillEffect> extraEffects = new();

    public void Reset()
    {
        bonusSkillDamage = 0;
        bonusSkillDamagePercent = 0;
        bonusBuffFlatDamage = 0;
        bonusBuffPercentDamage = 0;
        bonusDuration = 0;
        bonusCritChance = 0;
        bonusCritDamage = 0;
        bonusCost = 0;
        bonusCooldownReduction = 0;
        bonusStunDuration = 0;
        bonusHealFlat = 0;
        bonusHealPercent = 0;
        extraEffects.Clear();
    }
    public void AddEffect(SkillEffect effect)
    {
        if (effect == null) return;

        extraEffects.Add(effect);
    }
    public void AddDamage(float value)
    {
        bonusSkillDamage += value;
    }
    public void AddDamagePercent(float value)
    {
        bonusSkillDamagePercent += value;
    }
    public void AddBuffFlatDamage(float value)
    {
        bonusBuffFlatDamage += value;
    }
    public void AddBuffPercentDamage(float value)
    {
        bonusBuffPercentDamage += value;
    }
    public void AddDuration(float value)
    {
        bonusDuration += value;
    }
    public void AddCritChance(float value)
    {
        bonusCritChance += value;
    }
    public void AddCritDamage(float value)
    {
        bonusCritDamage += value;
    }
    public void AddCooldownReduction(float value)
    {
        bonusCooldownReduction += value;
    }
    public void AddCost(float value)
    {
        bonusCost += value;
    }
    public void AddStunDuration(float value)
    {
        bonusStunDuration += value;
    }
    public void AddHealFlat(float value)
    {
        bonusHealFlat += value;
    }
    public void AddHealPercent(float value)
    {
        bonusHealPercent += value;
    }
    public float BonusSkillDamage => bonusSkillDamage;
    public float BonusSkillDamagePercent => bonusSkillDamagePercent;
    public float BonusDuration => bonusDuration;
    public float BonusCritChance => bonusCritChance;
    public float BonusCritDamage => bonusCritDamage;
    public float BonusFlatDamage => bonusBuffFlatDamage;
    public float BonusPercentDamage => bonusBuffPercentDamage;
    public float BonusCost => bonusCost;
    public float BonusCooldownReduction => bonusCooldownReduction;
    public float BonusStunDuration => bonusStunDuration;
    public float BonusHealFlat => bonusHealFlat;
    public float BonusHealPercent => bonusHealPercent;
    public IReadOnlyList<SkillEffect> ExtraEffects => extraEffects;
}