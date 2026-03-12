using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class DamSenderBase : CharacterAbstract 
{
    [SerializeField] protected DamageInfo damageInfo;
    [SerializeField] private bool isActive = false;
    [SerializeField] private HashSet<DamReceiverBase> damagedTargets = new HashSet<DamReceiverBase>();
    protected override void ResetValue()
    {
        base.ResetValue();
        isActive = false;
        damagedTargets.Clear();
    }
    protected abstract void SpawnHitEffect(Collider other);
    public void EnableHitbox()
    {
        isActive = true;
        damagedTargets.Clear();
    }

    public void DisableHitbox()
    {
        isActive = false;
        damagedTargets.Clear();
    }
    public virtual void DealDamage(DamReceiverBase target)
    {
        if (target == null) return;
        if (damageInfo.Attacker == null)
        {
            Debug.LogWarning("DamageInfo.Attacker is null. Cannot calculate damage.");
            return;
        }

        float baseDamage = damageInfo.Attacker.CharacterStat.Damage.FinalValue;
        float finalDamage = baseDamage * damageInfo.Multiplier;

        if (damageInfo.CanCrit)
        {
            float critChance = damageInfo.Attacker.CharacterStat.CritChance.FinalValue;
            float critDamage = damageInfo.Attacker.CharacterStat.CritDame.FinalValue;

            if (Random.value <= critChance)
                finalDamage *= critDamage;
        }

        target.ReceiveDamage(finalDamage);

        Debug.Log($"{gameObject.name} gây {finalDamage} sát thương lên {target.gameObject.name}");
    }
    public virtual void DealDamageOverTime(DamReceiverBase target, float totalDamage, float duration, int ticks = 5)
    {
        if (target == null) return;
        StartCoroutine(DamageOverTimeCoroutine(target, totalDamage, duration, ticks));
    }

    private IEnumerator DamageOverTimeCoroutine(DamReceiverBase target, float totalDamage, float duration, int ticks)
    {
        float damagePerTick = totalDamage / ticks;
        float interval = duration / ticks;

        for (int i = 0; i < ticks; i++)
        {
            if (target == null || target.isDead) yield break;
            target.ReceiveDamage(damagePerTick);
            yield return new WaitForSeconds(interval);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;

        var receiver = other.GetComponentInChildren<DamReceiverBase>();
        if (receiver.faction == this.characterCtrl.faction) return;
        if (!FactionManager.CanAttack(this.characterCtrl.faction, receiver.faction)) return;
        if (damagedTargets.Contains(receiver)) return;
        Debug.Log($"{gameObject.name} hit {receiver.gameObject.name}");
        SpawnHitEffect(other);
        DealDamage(receiver);
        damagedTargets.Add(receiver);
    }
    public void SetDamageInfo(DamageInfo info)
    {
        damageInfo = info;
    }
}
