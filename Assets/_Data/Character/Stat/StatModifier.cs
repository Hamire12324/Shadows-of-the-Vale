using UnityEngine;

[System.Serializable]
public class StatModifier
{
    [SerializeField] protected StatType statType;
    public StatType StatType => statType;
    [SerializeField] protected ModifierType modifierType;
    public ModifierType ModifierType => modifierType;
    [SerializeField] protected float value;
    public float Value => value;
    public object Source;
    public StatModifier(StatType statType, ModifierType modifierType, float value)
    {
        this.statType = statType;
        this.modifierType = modifierType;
        this.value = value;
    }
}