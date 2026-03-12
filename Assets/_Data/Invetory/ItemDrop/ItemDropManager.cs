using UnityEngine;

public class ItemDropManager : BaseSingleton<ItemDropManager>
{
    [SerializeField] protected ItemDropSpawnerCtrl itemDropSpawnerCtrl;
    public ItemDropSpawnerCtrl ItemDropSpawnerCtrl => itemDropSpawnerCtrl;

    public float spawnHeight = 1f;
    public float forceAmount = 5f;
    [SerializeField] private float worldSize = 1f;
    public float WorldSize => worldSize;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSpawner();
    }

    protected virtual void LoadSpawner()
    {
        if (itemDropSpawnerCtrl != null) return;

        itemDropSpawnerCtrl = GetComponentInChildren<ItemDropSpawnerCtrl>();
        Debug.Log(transform.name + ": LoadSpawner", gameObject);
    }
    public virtual void DropMany(
    ItemProfileSO profile,
    Vector3 dropPosition,
    int dropCount)
    {
        for (int i = 0; i < dropCount; i++)
        {
            Drop(profile, 1, dropPosition);
        }
    }
    public virtual void Drop(
        ItemProfileSO profile,
        int dropCount,
        Vector3 dropPosition,
        bool isAutoPickup = true)
    {
        if (profile == null) return;
        if (itemDropSpawnerCtrl.ItemDropPrefabs.prefabs[0] == null) return;

        Vector3 spawnPos = GetRandomSpawnPos(dropPosition);

        ItemDropCtrl newItem =
            itemDropSpawnerCtrl.Spawn(itemDropSpawnerCtrl.ItemDropPrefabs.prefabs[0], spawnPos);

        newItem.Init(profile, dropCount, isAutoPickup);

        NormalizeScale(newItem.transform, profile.WorldSize);

        ApplyDropForce(newItem);
    }

    private Vector3 GetRandomSpawnPos(Vector3 origin)
    {
        Vector2 circle = Random.insideUnitCircle * 2f;
        return origin + new Vector3(circle.x, spawnHeight, circle.y);
    }

    private void ApplyDropForce(ItemDropCtrl item)
    {
        Vector3 dir = Random.onUnitSphere;
        dir.y = Mathf.Abs(dir.y);

        item.Rigidbody.AddForce(dir * forceAmount, ForceMode.Impulse);
    }
    private void NormalizeScale(Transform root, float targetSize)
    {
        Renderer renderer = root.GetComponentInChildren<Renderer>();
        if (renderer == null) return;

        root.localScale = Vector3.one;

        Bounds bounds = renderer.bounds;

        float biggest = Mathf.Max(
            bounds.size.x,
            bounds.size.y,
            bounds.size.z);

        if (biggest <= 0.001f) return;

        float scaleFactor = targetSize / biggest;

        root.localScale = Vector3.one * scaleFactor;
    }
}