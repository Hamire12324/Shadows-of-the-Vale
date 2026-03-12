using System.Collections.Generic;

[System.Serializable]
public class NodeStat
{
    public StatType statType;
    public ModifierType modifierType;

    public List<float> values;
}