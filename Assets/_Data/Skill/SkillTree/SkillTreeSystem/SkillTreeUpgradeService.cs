using System.Collections.Generic;

public class SkillTreeUpgradeService
{
    private IReadOnlyDictionary<int, SkillNodeRuntime> nodes;
    private SkillTreePrerequisiteChecker prerequisiteChecker;
    private SkillTreeEffectApplier effectApplier;

    public SkillTreeUpgradeService(
        IReadOnlyDictionary<int, SkillNodeRuntime> nodes,
        SkillTreePrerequisiteChecker prerequisiteChecker)
    {
        this.nodes = nodes;
        this.prerequisiteChecker = prerequisiteChecker;
    }

    public bool UpgradeNode(SkillNodeData nodeData)
    {
        if (!nodes.TryGetValue(nodeData.id, out var node))
            return false;

        if (!node.CanUpgrade(PlayerManager.playerCtrl.CharacterLevel.SkillPoints))
            return false;

        if (!prerequisiteChecker.Check(nodeData))
            return false;

        node.Upgrade();

        return true;
    }
}