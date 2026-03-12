using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStat : BaseMonoBehaviour
{
    [Header("Offense")]
    public StatValue Damage;
    public StatValue CritChance;
    public StatValue CritDame;

    [Header("Defense")]
    public StatValue Armor;
    public StatValue MaxHP;

    protected virtual void ClearAllModifiers()
    {
        Damage?.ClearModifiers();
        Armor?.ClearModifiers();
        MaxHP?.ClearModifiers();
        CritChance?.ClearModifiers();
        CritDame?.ClearModifiers();
    }

    public virtual StatValue GetStat(StatType type)
    {
        return type switch
        {
            StatType.Damage => Damage,
            StatType.Armor => Armor,
            StatType.MaxHP => MaxHP,
            StatType.CritChance => CritChance,
            StatType.CritDamage => CritDame,
            _ => null
        };
    }

    public virtual void RecalculateAll(List<StatModifier> modifiers)
    {
        ClearAllModifiers();
        foreach (var mod in modifiers)
        {
            GetStat(mod.StatType)?.AddModifier(mod);
        }
    }
    public void ResetAllStats()
    {
        ClearAllModifiers();
    }
}