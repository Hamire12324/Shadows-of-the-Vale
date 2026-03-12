using System.Collections.Generic;

public class SkillTreeSaveLoadService
{
    private IReadOnlyDictionary<int, SkillNodeRuntime> nodes;
    private SkillTreeEffectApplier effectApplier;

    public SkillTreeSaveLoadService(
        IReadOnlyDictionary<int, SkillNodeRuntime> nodes,
        SkillTreeEffectApplier effectApplier)
    {
        this.nodes = nodes;
        this.effectApplier = effectApplier;
    }

    public SkillTreeSaveData GetSaveData()
    {
        SkillTreeSaveData data = new();
        data.nodeLevels = new Dictionary<int, int>();

        foreach (var pair in nodes)
        {
            data.nodeLevels[pair.Key] = pair.Value.Level;
        }

        return data;
    }

    public void LoadSaveData(SkillTreeSaveData data)
    {
        if (data == null) return;

        foreach (var pair in data.nodeLevels)
        {
            if (!nodes.TryGetValue(pair.Key, out var node))
                continue;

            node.Reset();

            for (int i = 0; i < pair.Value; i++)
            {
                node.Upgrade();
                effectApplier.ApplyNodeEffects(node);
            }
        }
    }
}