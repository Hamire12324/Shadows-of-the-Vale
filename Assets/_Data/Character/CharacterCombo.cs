using UnityEngine;
public abstract class CharacterCombo : CharacterAbstract
{
    [SerializeField] protected float comboResetDelay = 3f;
    [SerializeField] protected AttackProfile[] attackProfiles;
    [SerializeField] protected int comboStep = 0;
    protected float lastAttackTime;

    public bool HasQueuedCombo { get; protected set; }
    public int CurrentCombo => comboStep;
    public virtual void ConsumeQueuedCombo() => HasQueuedCombo = false;
    public virtual AttackProfile CurrentProfile
    {
        get
        {
            if (comboStep <= 0 || comboStep > attackProfiles.Length)
                return null;

            return attackProfiles[comboStep - 1];
        }
    }

    public virtual bool CanAdvanceCombo()
    {
        return comboStep < attackProfiles.Length;
    }

    public virtual void AdvanceCombo()
    {
        if (!CanAdvanceCombo()) return;

        comboStep++;
        lastAttackTime = Time.time;
    }

    public virtual void ResetCombo()
    {
        comboStep = 0;
        HasQueuedCombo = false;
    }

    public virtual void Tick()
    {
        if (comboStep == 0) return;

        if (Time.time - lastAttackTime > comboResetDelay)
            ResetCombo();
    }

    public void QueueCombo()
    {
        if (HasQueuedCombo) return;
        HasQueuedCombo = true;
    }
    public virtual DamageInfo BuildDamageInfo()
    {
        var profile = CurrentProfile;
        if (profile == null) return default;

        return new DamageInfo
        {
            Attacker = characterCtrl,
            Multiplier = profile.damageMultiplier,
            CanCrit = true,
            DamageType = DamageType.Physical
        };
    }
}