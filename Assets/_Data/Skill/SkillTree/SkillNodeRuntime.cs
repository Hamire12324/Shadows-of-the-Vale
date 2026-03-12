using System;
using UnityEngine;
public class SkillNodeRuntime
{
    public SkillNodeData Data { get; private set; }

    public int Level { get; private set; }

    public bool IsUnlocked => Level > 0;
    public int NextLevel => Level + 1;
    public bool IsMaxLevel => Level >= Data.maxLevel;

    public event Action<SkillNodeRuntime> OnLevelChanged;
    public SkillNodeRuntime(SkillNodeData data)
    {
        Data = data;
        Level = 0;
    }
    public bool CanUpgrade(int playerSkillPoint)
    {
        if (IsMaxLevel) return false;

        int cost = Data.GetUpgradeCost(Level);

        if (playerSkillPoint < cost)
            return false;

        return true;
    }
    public bool Upgrade()
    {
        int cost = Data.GetUpgradeCost(Level);

        var levelSystem = PlayerManager.playerCtrl.CharacterLevel;

        if (!CanUpgrade(levelSystem.SkillPoints)) return false;

        if (!levelSystem.SpendSkillPoint(cost)) return false;

        Level++;
        OnLevelChanged?.Invoke(this);

        return true;
    }
    public void Unlock()
    {
        if (Level > 0) return;

        Level = 1;

        OnLevelChanged?.Invoke(this);
    }
    public void Reset()
    {
        if (Level == 0) return;

        Level = 0;

        OnLevelChanged?.Invoke(this);
    }
}