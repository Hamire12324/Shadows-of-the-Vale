using UnityEngine;

public abstract class PoolObj : BaseMonoBehaviour
{
    [SerializeField] protected DespawnBase despawn;
    public DespawnBase Despawn => despawn;
    public PoolObj PrefabSource { get; private set; }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDespawn();
    }
    protected virtual void LoadDespawn()
    {
        if(this.despawn != null) return;
        this.despawn = transform.GetComponentInChildren<DespawnBase>();
    }
    public void SetPrefabSource(PoolObj prefab)
    {
        PrefabSource = prefab;
    }
}
