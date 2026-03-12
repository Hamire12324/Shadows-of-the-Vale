using UnityEngine;

public class EnemyDamReceiver : DamReceiverBase
{
    [SerializeField] private EnemyCtrl enemyCtrl;
    protected override void ResetValue()
    {
        base.ResetValue();
        this.faction = Faction.Enemy;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (enemyCtrl == null) enemyCtrl = GetComponentInParent<EnemyCtrl>();
    }

    public override void ReceiveDamage(float damage)
    {
        base.ReceiveDamage(damage);

        enemyCtrl.EnemyVision.ForceVision(3f);

        if (IsDead()) OnDead();
    }

    public virtual bool IsDead()
    {
        return this.isDead = this.currentHp <= 0;
    }

    protected override void OnDead()
    {
        base.OnDead();
        this.Die();
        this.RewardOnDead();
        enemyCtrl.Agent.enabled = false;
        Invoke(nameof(this.Disappear), 3f);
    }
    protected virtual void Disappear()
    {
        this.enemyCtrl.Despawn.DoDespawn();
    }
    protected virtual void RewardOnDead()
    {

    }
}