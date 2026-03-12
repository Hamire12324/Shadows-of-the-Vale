using System.Collections.Generic;
using UnityEngine;

public class SkillManager : PlayerAbstract
{
    [SerializeField] private SkillTreeData skillTreeData;
    private SkillRuntimeSystem skillRuntimeSystem;
    public SkillRuntimeSystem SkillRuntimeSystem => skillRuntimeSystem;

    private SkillUnlockSystem skillUnlockSystem;
    public SkillUnlockSystem SkillUnlockSystem => skillUnlockSystem;

    private SkillTreeSystem skillTreeSystem;
    public SkillTreeSystem SkillTreeSystem => skillTreeSystem;

    private SkillExecutionSystem skillExecutionSystem;
    public SkillExecutionSystem SkillExecutionSystem => skillExecutionSystem;

    private ResourceManager resourceManager;
    public ResourceManager ResourceManager => resourceManager;
    [SerializeField] private List<SkillData> defaultEquippedSkills;
    protected override void Awake()
    {
        base.Awake();

        resourceManager = new ResourceManager(playerCtrl);

        skillRuntimeSystem = new SkillRuntimeSystem(this);

        skillTreeSystem = new SkillTreeSystem(playerCtrl, skillRuntimeSystem, 
            playerCtrl.SkillTreeManager);

        skillUnlockSystem = new SkillUnlockSystem(playerCtrl, skillTreeSystem);

        skillExecutionSystem = new SkillExecutionSystem(playerCtrl);

        skillRuntimeSystem.Load(defaultEquippedSkills);

        Debug.Log("Runtime skills loaded: " + defaultEquippedSkills.Count);
    }
    private void OnPlayerReady()
    {
        resourceManager.InitResources();
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();

        PlayerCtrl.OnPlayerReady -= OnPlayerReady;
    }
    public void TryCast(int index)
    {
        SkillRuntime skill = skillRuntimeSystem.GetRuntimeSkills(index);

        if (skill == null) return;

        skill.Cast();
    }
    public bool UnlockNode(SkillNodeData node)
    {
        bool success = skillUnlockSystem.Unlock(node);

        if (success)
        {
            if (node.skillData != null && node.nodeType == SkillNodeType.SkillUnlock) 
                skillRuntimeSystem.AddSkill(node.skillData);

            var runtime = skillTreeSystem.GetRuntimeNode(node);
            runtime.Unlock();
            skillTreeSystem.RebuildAll();
        }

        return success;
    }
    public void UpgradeSkillNode(SkillNodeData node)
    {
        bool success = skillTreeSystem.Upgrade.UpgradeNode(node);

        if (success) skillTreeSystem.RebuildAll();
    }
}