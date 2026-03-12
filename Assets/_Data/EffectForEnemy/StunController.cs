using UnityEngine;
using System.Collections;

public class StunController : BaseMonoBehaviour
{
    [SerializeField] protected EnemyCtrl enemyCtrl;
    [SerializeField] protected bool isStunned;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyCtrl();
    }
    protected virtual void LoadEnemyCtrl()
    {
        if (enemyCtrl != null) return;
        enemyCtrl = GetComponentInParent<EnemyCtrl>();
    }
    public void ApplyStun(float duration)
    {
        if (!gameObject.activeInHierarchy) return;

        StartCoroutine(StunRoutine(duration));
    }

    IEnumerator StunRoutine(float duration)
    {
        isStunned = true;

        enemyCtrl.EnemyMovement.enabled = false;

        yield return new WaitForSeconds(duration);

        enemyCtrl.EnemyMovement.enabled = true;

        isStunned = false;
    }
}