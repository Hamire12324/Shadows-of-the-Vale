using System.Collections.Generic;

public class SkillTreeQueryService
{
    private IReadOnlyDictionary<int, SkillNodeRuntime> nodes;

    public SkillTreeQueryService(IReadOnlyDictionary<int, SkillNodeRuntime> nodes)
    {
        this.nodes = nodes;
    }

    public SkillNodeRuntime GetNode(int id)
    {
        nodes.TryGetValue(id, out var node);
        return node;
    }

    public bool IsUnlocked(int id)
    {
        var node = GetNode(id);
        return node != null && node.IsUnlocked;
    }
}