using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Attack Profile")]
public class AttackProfile : ScriptableObject
{
    [Header("Damage")]
    public float damageMultiplier = 1f;
    public DamageType damageType = DamageType.Physical;
    public bool canCrit = true;

    [Header("Effects")]
    public EffectData slashEffect;
    public EffectData hitEffect;
}