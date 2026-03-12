using UnityEngine;

public class ItemDropSpawnerCtrl : Spawner<ItemDropCtrl>
{
    [SerializeField] protected ItemDropPrefabs itemDropPrefabs;
    public ItemDropPrefabs ItemDropPrefabs => itemDropPrefabs;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPoolPrefabs();
    }

    protected virtual void LoadPoolPrefabs()
    {
        if (this.itemDropPrefabs != null) return;
        this.itemDropPrefabs = GetComponentInChildren<ItemDropPrefabs>();
        Debug.Log(transform.name + ": LoadPoolPrefabs", gameObject);
    }
}
