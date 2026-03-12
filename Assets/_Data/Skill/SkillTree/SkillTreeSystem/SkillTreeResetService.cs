using System.Collections.Generic;

public class SkillTreeResetService
{
    private IReadOnlyDictionary<int, SkillNodeRuntime> nodes;

    public SkillTreeResetService(IReadOnlyDictionary<int, SkillNodeRuntime> nodes)
    {
        this.nodes = nodes;
    }

    public void ResetTree()
    {
        foreach (var node in nodes.Values)
        {
            node.Reset();
        }
    }
}