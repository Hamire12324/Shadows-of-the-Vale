using UnityEngine;

public class WeaponAbstract : BaseMonoBehaviour
{
    [SerializeField] protected AttackPoint attackPoint;
    public AttackPoint AttackPoint => attackPoint;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAttackPoint();
    }
    protected virtual void LoadAttackPoint()
    {
        if (this.attackPoint != null) return;
        this.attackPoint = transform.GetComponentInChildren<AttackPoint>();
        Debug.Log(transform.name + ": LoadAnimator", gameObject);
    }
}
