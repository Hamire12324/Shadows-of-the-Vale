using UnityEngine;

public class EnemyAttackController : CharacterAbstract
{
    [SerializeField] private float attackDelay = 5f;

    private float nextAttackTime = 0f;
    public bool CanAttack()
    {
        return Time.time >= nextAttackTime;
    }
    public void TriggerAttack()
    {
        if (!CanAttack()) return;

        nextAttackTime = Time.time + attackDelay;
        characterCtrl.Animator.SetTrigger("Attack");
    }
    public void Animation_EnableDamage()
    {
        characterCtrl.WeaponManager.EnableDamage();
    }

    public void Animation_DisableDamage()
    {
        characterCtrl.WeaponManager.DisableDamage();
    }
}