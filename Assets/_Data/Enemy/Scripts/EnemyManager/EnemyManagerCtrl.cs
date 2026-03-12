using UnityEngine;

public class EnemyManagerCtrl : BaseMonoBehaviour
{
    [SerializeField] protected EnemySpawner enemySpawner;
    public EnemySpawner EnemySpawner => enemySpawner;

    [SerializeField] protected EnemyPrefabs enemyPrefabs;
    public EnemyPrefabs EnemyPrefabs => enemyPrefabs;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemySpawner();
        this.LoadEnemyPrefabs();
    }
    protected virtual void LoadEnemySpawner()
    {
        if (enemySpawner != null) return;
        this.enemySpawner = GetComponentInChildren<EnemySpawner>();
        Debug.Log(transform.name + ": LoadEnemySpawner", gameObject);
    }
    protected virtual void LoadEnemyPrefabs()
    {
        if (enemyPrefabs != null) return;
        this.enemyPrefabs = GetComponentInChildren<EnemyPrefabs>();
        Debug.Log(transform.name + ": LoadEnemyPrefabs", gameObject);
    }
}
