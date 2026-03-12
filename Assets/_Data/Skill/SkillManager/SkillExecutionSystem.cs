using UnityEngine;
public class SkillExecutionSystem
{
    private CharacterCtrl character;

    public SkillExecutionSystem(CharacterCtrl character)
    {
        this.character = character;
    }

    public void Cast(SkillRuntime skill)
    {
        if (skill == null) return;

        foreach (var effect in skill.SkillData.effects)
        {
            effect.Execute(character, null, skill);
        }

        foreach (var effect in skill.SkillModifier.ExtraEffects)
        {
            effect.Execute(character, null, skill);
        }
    }
}