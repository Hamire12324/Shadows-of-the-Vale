using System.Collections.Generic;

public class SkillTreeSystem
{
    private readonly IReadOnlyDictionary<int, SkillNodeRuntime> nodes;
    private readonly SkillTreeEffectApplier effectApplier;
    private readonly SkillRuntimeSystem runtimeSystem;
    private readonly CharacterCtrl character;

    public SkillTreeQueryService Query { get; }
    public SkillTreeUpgradeService Upgrade { get; }
    public SkillTreeSaveLoadService SaveLoad { get; }
    public SkillTreeResetService Reset { get; }

    public SkillTreeSystem(
        CharacterCtrl character,
        SkillRuntimeSystem runtimeSystem,
        SkillTreeManager treeManager)
    {
        this.character = character;
        this.runtimeSystem = runtimeSystem;

        nodes = treeManager.Nodes;

        var prerequisite = new SkillTreePrerequisiteChecker(nodes);
        effectApplier = new SkillTreeEffectApplier(character, runtimeSystem);

        Query = new SkillTreeQueryService(nodes);
        Upgrade = new SkillTreeUpgradeService(nodes, prerequisite);
        SaveLoad = new SkillTreeSaveLoadService(nodes, effectApplier);
        Reset = new SkillTreeResetService(nodes);
    }
    public void RebuildAll()
    {
        RebuildSkillModifiers();
        RebuildCharacterStats();
        RebuildExtraEffects();
    }
    public void RebuildSkillModifiers()
    {
        foreach (var runtime in runtimeSystem.GetAllRuntimeSkills())
        {
            runtime.SkillModifier.Reset();
        }

        foreach (var node in nodes.Values)
        {
            if (node.Level > 0)
                effectApplier.ApplySkillModifiers(node);
        }
        foreach (var runtime in runtimeSystem.GetAllRuntimeSkills())
        {
            runtime.NotifyRuntimeChanged();
        }
    }
    public void RebuildCharacterStats()
    {
        var statComp = character.CharacterStat;

        statComp.ResetAllStats();

        foreach (var node in nodes.Values)
        {
            if (node.Level > 0)
                effectApplier.ApplyStatModifiers(node);
        }
    }
    public void RebuildExtraEffects()
    {
        foreach (var node in nodes.Values)
        {
            if (node.Level > 0)
                effectApplier.ApplyExtraEffects(node.Data);
        }
    }
    public SkillNodeRuntime GetRuntimeNode(SkillNodeData data)
    {
        nodes.TryGetValue(data.id, out var runtime);
        return runtime;
    }
}