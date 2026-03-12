using System.Collections.Generic;
using UnityEngine;

public class EffectManager : BaseSingleton<EffectManager>
{
    [SerializeField] protected EffectSpawner effectSpawner;
    public EffectSpawner EffectSpawner => effectSpawner;

    [SerializeField] protected EffectPrefabs prefabs;
    public EffectPrefabs Prefabs => prefabs;
    [SerializeField] protected PoolPrefabs<EffectCtrl> poolPrefabs;
    public PoolPrefabs<EffectCtrl> PoolPrefabs => poolPrefabs;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEffectSpawner();
        this.LoadEffectPrefabs();
        this.LoadPoolPrefabs();
    }

    protected virtual void LoadEffectSpawner()
    {
        if (this.effectSpawner != null) return;
        this.effectSpawner = GetComponentInChildren<EffectSpawner>();
        Debug.Log(transform.name + ": LoadEffectSpawner", gameObject);
    }

    protected virtual void LoadEffectPrefabs()
    {
        if (this.prefabs != null) return;
        this.prefabs = GetComponentInChildren<EffectPrefabs>();
        Debug.Log(transform.name + ": LoadEffectPrefabs", gameObject);
    }
    protected virtual void LoadPoolPrefabs()
    {
        if (this.poolPrefabs != null) return;
        this.poolPrefabs = GetComponentInChildren<PoolPrefabs<EffectCtrl>>();
        Debug.Log(transform.name + ": LoadPoolPrefabs", gameObject);
    }
    public EffectCtrl SpawnHit(EffectData data, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (data == null || data.prefab == null) return null;

        Quaternion rot = Quaternion.LookRotation(hitNormal);

        return SpawnEffect(data, hitPoint, rot);
    }
    public EffectCtrl SpawnEffect(EffectData data, Vector3 pos, Quaternion rot, 
        Transform followTarget = null, float lifeTime = -1f)
    {
        if (data == null || data.prefab == null) return null;

        EffectCtrl effect = effectSpawner.Spawn(data.prefab);

        foreach (Transform t in effect.transform)
        {
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
        }

        effect.transform.SetPositionAndRotation(pos, rot);
        effect.gameObject.SetActive(true);

        if (followTarget != null)
        {
            FollowTarget follow = effect.GetComponentInChildren<FollowTarget>();
            if (follow != null) follow.SetTarget(followTarget);
        }

        var despawn = effect.GetComponentInChildren<DespawnBase>();

        if (despawn != null)
        {
            despawn.OnSpawn();

            if (lifeTime > 0)
            {
                var despawnTime = despawn as Despawn<EffectCtrl>;
                despawnTime?.SetLifeTime(lifeTime);
            }
        }

        return effect;
    }
}
