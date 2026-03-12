using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SkillEffect/StunEffect")]
public class StunEffect : SkillEffect
{
    public float stunDuration = 1.5f;

    public override void Execute(CharacterCtrl caster, CharacterCtrl target, SkillRuntime skill)
    {
        if (target == null) return;

        float duration = skill.SkillModifier.BonusStunDuration;

        StunController stun = target.GetComponentInChildren<StunController>();

        if (stun != null)
            stun.ApplyStun(duration);
    }
}