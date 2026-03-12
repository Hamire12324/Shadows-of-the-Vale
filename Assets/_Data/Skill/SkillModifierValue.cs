using System.Collections.Generic;

[System.Serializable]
public class SkillModifierValue
{
    public SkillModifierType type;
    public List<float> values = new();
}