using UnityEngine;

public abstract class SkillEffect : ScriptableObject
{
    public abstract void Execute(CharacterCtrl caster, CharacterCtrl target, SkillRuntime skill);
}