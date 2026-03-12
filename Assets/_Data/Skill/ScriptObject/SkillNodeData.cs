using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillNodeData
{
    public int id;

    public string nodeName;
    public string description;

    public Vector2 position;

    public Sprite icon;

    public SkillNodeType nodeType;

    public int maxLevel = 1;

    public List<int> prerequisites = new List<int>();

    public List<int> upgradeCosts = new();

    public SkillData skillData;

    public List<NodeRequirement> requiredNodes = new();

    public List<SkillEffect> upgradeEffects = new();

    public List<SkillModifierValue> modifiers = new();

    public List<NodeStat> statModifiers = new();
    public int GetUpgradeCost(int currentLevel)
    {
        if (upgradeCosts == null || upgradeCosts.Count == 0)
            return 0;

        if (currentLevel >= upgradeCosts.Count)
            return upgradeCosts[upgradeCosts.Count - 1];

        return upgradeCosts[currentLevel];
    }
}