using System.Collections.Generic;
using UnityEngine;

public class PoolPrefabs<T> : BaseMonoBehaviour where T : MonoBehaviour
{
    [SerializeField] public List<T> prefabs = new();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPrefabs();
        HidePrefabs();
    }

    protected virtual void LoadPrefabs()
    {
        if (prefabs.Count > 0) return;

        foreach (Transform child in transform)
        {
            T p = child.GetComponent<T>();
            if (p) prefabs.Add(p);
        }
    }

    protected virtual void HidePrefabs()
    {
        foreach (T prefab in prefabs)
            prefab.gameObject.SetActive(false);
    }

    public T GetRandom() => prefabs[Random.Range(0, prefabs.Count)];
    public List<T> GetAll() => prefabs;
}
