using UnityEngine;

public class SkillUnlockSystem
{
    private CharacterCtrl character;
    private SkillTreeSystem skillTreeSystem;

    public SkillUnlockSystem(CharacterCtrl character, SkillTreeSystem tree)
    {
        this.character = character;
        this.skillTreeSystem = tree;
    }

    public bool Unlock(SkillNodeData nodeData)
    {
        if (nodeData == null) return false;

        var node = skillTreeSystem.Query.GetNode(nodeData.id);

        if (node == null) return false;
        if (node.IsUnlocked) return false;
        if (!CheckPrerequisites(nodeData)) return false;

        var levelSystem = character.CharacterLevel;

        int cost = nodeData.GetUpgradeCost(0);

        if (!levelSystem.SpendSkillPoint(cost)) return false;

        node.Unlock();

        return true;
    }

    bool CheckPrerequisites(SkillNodeData nodeData)
    {
        if (nodeData.requiredNodes == null || nodeData.requiredNodes.Count == 0)
            return true;

        foreach (var reqData in nodeData.requiredNodes)
        {
            var reqNode = skillTreeSystem.Query.GetNode(reqData.nodeId);

            if (reqNode == null)
                return false;

            if (!reqNode.IsUnlocked)
                return false;

            if (reqNode.Level < reqData.requiredLevel)
                return false;
        }

        return true;
    }
}