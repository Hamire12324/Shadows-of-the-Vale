using UnityEngine;
using System.Collections.Generic;
public class EnemyPrefabs : EnemyManagerAbstract
{
    [SerializeField] protected List<EnemyCtrl> prefabs = new();
    protected override void Awake()
    {
        base.Awake();
        //this.HidePrefabs();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyPrefabs();
    }
    protected virtual void LoadEnemyPrefabs()
    {
        if (this.prefabs.Count > 0) return;
        foreach (Transform child in transform)
        {
            EnemyCtrl enemyCtrl = child.GetComponent<EnemyCtrl>();
            if (enemyCtrl) this.prefabs.Add(enemyCtrl);
        }
        Debug.Log(transform.name + ": LoadPrefabs", gameObject);
    }
    protected virtual void HidePrefabs()
    {
        foreach(EnemyCtrl enemyctrl in this.prefabs)
        {
            enemyctrl.gameObject.SetActive(false);
        }
    }
    public virtual EnemyCtrl GetRamdom()
    {
        int rand = Random.Range(0, this.prefabs.Count);
        return this.prefabs[rand];
    }
}
