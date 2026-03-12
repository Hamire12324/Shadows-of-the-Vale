using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Skills/Skill")]
public class SkillData : ScriptableObject
{
    [Header("Info")]
    public string skillID;
    public string skillName;
    public Sprite icon;

    [Header("Unlock")]
    public int requiredLevel;
    public int skillCost = 1;

    [Header("Resource Cost")]
    public ResourceType resourceType;
    public float cost;

    [Header("Cooldown")]
    public float cooldown;

    [Header("Cast")]
    public SkillCastType castType;
    public float range;

    [Header("Animation")]
    public int animationIndex;

    [Header("Effects")]
    public List<SkillEffect> effects;
}