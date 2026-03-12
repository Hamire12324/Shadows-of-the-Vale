[System.Serializable]
public class StatModifierData
{
    public StatType statType;
    public ModifierType modifierType;
    public float value;

    public StatModifier ToRuntimeModifier()
    {
        return new StatModifier(statType, modifierType, value);
    }
}