using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatValue
{
    [SerializeField] private float baseValue;

    private readonly List<StatModifier> modifiers = new();

    private bool isDirty = true;
    private float cachedFinalValue;

    public float BaseValue
    {
        get => baseValue;
        set
        {
            baseValue = value;
            isDirty = true;
        }
    }

    public float FinalValue
    {
        get
        {
            if (isDirty) Recalculate();
            return cachedFinalValue;
        }
    }

    public void AddModifier(StatModifier modifier)
    {
        modifiers.RemoveAll(m => m.Source == modifier.Source && m.ModifierType == modifier.ModifierType);

        modifiers.Add(modifier);
        isDirty = true;
    }
    public void RemoveModifier(StatModifier modifier)
    {
        modifiers.Remove(modifier);
        isDirty = true;
    }
    public void ClearModifiers()
    {
        modifiers.Clear();
        isDirty = true;
    }

    private void Recalculate()
    {
        float flat = 0f;
        float percent = 0f;
        foreach (var mod in modifiers)
        {
            Debug.Log("MOD FOUND: " + mod.Value + " | " + mod.ModifierType);

            if (mod.ModifierType == ModifierType.Flat)
                flat += mod.Value;
            else
                percent += mod.Value;
        }

        cachedFinalValue = baseValue * (1f + percent) + flat;
        isDirty = false;
    }
    public void RemoveModifierFromSource(object source)
    {
        modifiers.RemoveAll(m => m.Source == source);
        isDirty = true;
    }
}