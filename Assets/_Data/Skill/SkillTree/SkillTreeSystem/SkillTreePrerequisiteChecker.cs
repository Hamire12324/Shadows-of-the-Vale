using System.Collections.Generic;
using UnityEngine;
public class SkillTreePrerequisiteChecker
{
    private IReadOnlyDictionary<int, SkillNodeRuntime> nodes;

    public SkillTreePrerequisiteChecker(IReadOnlyDictionary<int, SkillNodeRuntime> nodes)
    {
        this.nodes = nodes;
    }

    public bool Check(SkillNodeData nodeData)
    {
        if (nodeData.requiredNodes == null)
            return true;

        foreach (var req in nodeData.requiredNodes)
        {
            if (!nodes.TryGetValue(req.nodeId, out var node))
                return false;

            if (!node.IsUnlocked)
                return false;

            if (node.Level < req.requiredLevel)
                return false;
        }

        return true;
    }
}