using UnityEngine;

public abstract class DamReceiverBase : BaseMonoBehaviour
{
    [SerializeField] protected CharacterCtrl characterCtrlBase;
    public CharacterCtrl CharacterCtrlBase => characterCtrlBase;
    [SerializeField] public Faction faction;
    [SerializeField] public float currentHp;
    [SerializeField] public bool isDead = false;
    [SerializeField] protected bool isInvincible;
    public bool IsInvincible => isInvincible;
    [SerializeField] protected float lastMaxHp;
    public delegate void OnDeathDelegate(DamReceiverBase self);
    public event OnDeathDelegate OnDeath;
    public virtual float MaxHp => characterCtrlBase.CharacterStat.MaxHP.FinalValue;
    protected override void ResetValue()
    {
        base.ResetValue();

        lastMaxHp = MaxHp;
        currentHp = lastMaxHp;

        isDead = false;
        isInvincible = false;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCharacterCtrlBase();
    }
    protected virtual void LoadCharacterCtrlBase()
    {
        if (this.characterCtrlBase != null) return;
        this.characterCtrlBase = GetComponentInParent<CharacterCtrl>();
        Debug.Log(transform.name + ": LoadCharacterController", gameObject);
    }
    public virtual void ReceiveDamage(float damage)
    {
        Debug.Log($"{transform.name} received {damage} damage.", gameObject);
        if (isDead) return;
        if (isInvincible) return;
        currentHp -= damage;

        DamageTextManager.Instance.SpawnDamage(damage,transform.position);

        if (currentHp <= 0)
        {
            currentHp = 0;
            Die();
            return;
        }
        characterCtrlBase.Animator.SetTrigger("Hit");
    }
    protected virtual void Die()
    {
        if (isDead) return;

        isDead = true;

        characterCtrlBase.Animator.ResetTrigger("Hit");
        characterCtrlBase.Animator.SetTrigger("Die");

        OnDeath?.Invoke(this);
    }

    public virtual void Heal(float amount)
    {
        currentHp += amount;
        if (currentHp > MaxHp) currentHp = MaxHp;
    }
    protected virtual void OnDead()
    {
        //For override
    }
    public void OnMaxHpChanged()
    {
        float newMaxHp = MaxHp;

        float percent = currentHp / lastMaxHp;

        currentHp = newMaxHp * percent;

        currentHp = Mathf.Clamp(currentHp, 0, newMaxHp);
        Debug.Log("Max HP changed. Current HP adjusted to maintain percentage: " + currentHp);
        Debug.Log("Old MaxHp: " + lastMaxHp);
        Debug.Log("New MaxHp: " + newMaxHp);
        lastMaxHp = newMaxHp;
    }
    public void SetInvincible(bool value)
    {
        isInvincible = value;
    }
}
