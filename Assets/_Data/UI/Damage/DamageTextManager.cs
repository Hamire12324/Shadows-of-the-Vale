using UnityEngine;

public class DamageTextManager : BaseSingleton<DamageTextManager>
{
    [SerializeField] protected DamageTextSpawner spawner;
    [SerializeField] protected DamageTextCtrl damagePrefab;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawner();
        this.LoadDamagePrefab();
    }

    protected virtual void LoadSpawner()
    {
        if (spawner != null) return;
        spawner = GetComponentInChildren<DamageTextSpawner>();
    }
    protected virtual void LoadDamagePrefab()
    {
        if (this.damagePrefab != null) return;
        this.damagePrefab = GetComponentInChildren<DamageTextCtrl>();
    }
    public DamageTextCtrl SpawnDamage(float damage, Vector3 worldPos)
    {
        DamageTextCtrl txt = spawner.Spawn(damagePrefab);

        Vector3 offset = new Vector3(
            Random.Range(-0.25f, 0.25f),
            Random.Range(1.6f, 2.1f),
            0
        );
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos + offset);

        txt.transform.position = screenPos;
        txt.gameObject.SetActive(true);

        txt.SetDamage(damage);

        return txt;
    }
}