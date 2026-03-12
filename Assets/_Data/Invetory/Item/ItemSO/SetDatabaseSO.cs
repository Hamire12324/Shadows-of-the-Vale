using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Items/SetDatabase")]
public class SetDatabaseSO : ScriptableObject
{
    [SerializeField] private List<SetProfileSO> sets;

    private Dictionary<string, SetProfileSO> cache;

    private void OnEnable()
    {
        cache = new Dictionary<string, SetProfileSO>();

        foreach (var set in sets)
        {
            if (!cache.ContainsKey(set.SetID))
                cache.Add(set.SetID, set);
        }
    }

    public SetProfileSO GetSet(string id)
    {
        if (cache.TryGetValue(id, out var result))
            return result;

        return null;
    }
}