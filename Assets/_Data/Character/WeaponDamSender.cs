using UnityEngine;

public class WeaponDamSender : DamSenderBase
{
    private Collider weaponCollider;
    private EffectData currentHitEffect;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        weaponCollider = GetComponent<Collider>();
        weaponCollider.enabled = false;
    }
    public void Init(CharacterCtrl owner)
    {
        this.characterCtrl = owner;
    }
    public void EnableDamage()
    {
        weaponCollider.enabled = true;
        EnableHitbox();
    }

    public void DisableDamage()
    {
        weaponCollider.enabled = false;
        DisableHitbox();
    }

    protected override void SpawnHitEffect(Collider other)
    {
        if (currentHitEffect == null) return;

        Vector3 hitPoint = other.ClosestPoint(transform.position);
        Vector3 hitNormal = (other.transform.position - transform.position).normalized;

        EffectManager.Instance.SpawnHit(
            currentHitEffect,
            hitPoint,
            hitNormal
        );
    }
    public void SetHitEffect(EffectData effect)
    {
        currentHitEffect = effect;
    }
}