using UnityEngine;

public class DamageTextSpawner : Spawner<DamageTextCtrl>
{
    public override DamageTextCtrl Spawn(DamageTextCtrl prefab)
    {
        DamageTextCtrl newObject = GetObjFromPool(prefab);

        if (newObject == null)
        {
            newObject = Instantiate(prefab);
            newObject.SetPrefabSource(prefab);
            this.spawnCount++;
            this.UpdateName(prefab.transform, newObject.transform);
        }

        if (this.poolHolder != null)
            newObject.transform.SetParent(this.poolHolder.transform, false);

        return newObject;
    }
}