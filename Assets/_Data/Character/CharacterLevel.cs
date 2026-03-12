using System;
using UnityEngine;

public class CharacterLevel : PlayerAbstract
{
    [SerializeField] private int level = 1;
    public int Level => level;
    [SerializeField] private int skillPoints;
    public int SkillPoints => skillPoints;
    [SerializeField] private int currentExp;
    [SerializeField] private int expToNext = 100;

    public event Action<int> OnSkillPointChanged;

    public void AddExp(int amount)
    {
        currentExp += amount;

        while (currentExp >= expToNext)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentExp -= expToNext;
        level++;

        skillPoints++;

        Debug.Log("Level Up: " + level);
    }
    public void AddSkillPoint(int amount)
    {
        skillPoints += amount;
        OnSkillPointChanged?.Invoke(SkillPoints);
    }
    public bool SpendSkillPoint(int cost)
    {
        if (skillPoints < cost) return false;

        skillPoints -= cost;
        OnSkillPointChanged?.Invoke(SkillPoints);
        return true;
    }
}