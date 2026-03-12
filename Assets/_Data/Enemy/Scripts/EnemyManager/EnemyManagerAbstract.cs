using UnityEngine;

public abstract class EnemyManagerAbstract : BaseMonoBehaviour
{
    [SerializeField] protected EnemyManagerCtrl enemyManagerCtrl;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyManagerCtrl();
    }
    protected virtual void LoadEnemyManagerCtrl()
    {
        if (enemyManagerCtrl != null) return;
        this.enemyManagerCtrl = transform.parent.GetComponent<EnemyManagerCtrl>();
        Debug.Log(transform.name + ": LoadEnemyManagerCtrl", gameObject);
    }
}
