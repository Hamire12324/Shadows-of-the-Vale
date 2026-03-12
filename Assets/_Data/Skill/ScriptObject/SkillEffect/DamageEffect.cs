using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/SkillEffect/DamageEffect")]
public class DamageEffect : SkillEffect
{
    public float multiplier = 1f;
    public bool canCrit = true;
    public DamageType damageType;

    [Header("Duration")]
    public float duration = 3f;
    public float tickInterval = 0.5f;

    public EffectData slashEffect;
    public EffectData hitEffect;

    [Header("Timing")]
    public float delayBeforeImpact = 0.8f;

    [Header("Attack Shape")]
    public float radius = 3f;

    [Range(0, 360)]
    public float attackAngle = 90f;

    public override void Execute(CharacterCtrl caster, CharacterCtrl target, SkillRuntime skill)
    {
        DamageEffectExecutor.Execute(this, caster, skill);
    }
}