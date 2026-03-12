using System.Collections.Generic;
using UnityEngine;

public class SkillTreeManager : BaseMonoBehaviour
{
    [SerializeField] private SkillTreeData treeData;

    private Dictionary<int, SkillNodeRuntime> runtimeNodes = new();

    public IReadOnlyDictionary<int, SkillNodeRuntime> Nodes => runtimeNodes;

    protected override void Awake()
    {
        BuildRuntime();
    }

    void BuildRuntime()
    {
        runtimeNodes.Clear();

        foreach (var node in treeData.nodes)
        {
            runtimeNodes[node.id] = new SkillNodeRuntime(node);
        }
    }

    public bool UpgradeNode(int id)
    {
        if (!runtimeNodes.ContainsKey(id)) return false;

        var node = runtimeNodes[id];

        if (!node.CanUpgrade(PlayerManager.playerCtrl.CharacterLevel.SkillPoints)) return false;

        node.Upgrade();

        return true;
    }
}