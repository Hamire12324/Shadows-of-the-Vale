using UnityEngine;
using System.Collections.Generic;
public class EnemySpawning : EnemyManagerAbstract
{
    [SerializeField] protected float spawnSpeed = 5f;
    [SerializeField] protected int maxSpawn = 10;
    protected List<EnemyCtrl> spawnedEnemies = new();
    protected override void Start()
    {
        base.Start();
        Invoke(nameof(this.Spawning),this.spawnSpeed);
    }
    protected override void FixedUpdate()
    {
        this.RemoveDeadOne();
    }
    protected virtual void Spawning()
    {
        Invoke(nameof(this.Spawning), this.spawnSpeed);
        if (this.spawnedEnemies.Count >= this.maxSpawn) return;
        EnemyCtrl prefab = this.GetEnemyPrefabs();
        EnemyCtrl newEnemy = this.enemyManagerCtrl.EnemySpawner.Spawn(prefab, transform.position);
        newEnemy.gameObject.SetActive(true);

        this.spawnedEnemies.Add(newEnemy);
    }
    protected virtual void RemoveDeadOne()
    {
        foreach (EnemyCtrl enemyCtrl in this.spawnedEnemies)
        {
            if (enemyCtrl.EnemyDamReceiver.IsDead())
            {
                this.spawnedEnemies.Remove(enemyCtrl);
                return;
            }
        }
    }
    protected virtual EnemyCtrl GetEnemyPrefabs()
    {
        return this.enemyManagerCtrl.EnemyPrefabs.GetRamdom();
    }
}
