using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Skill/Skill Tree")]
public class SkillTreeData : ScriptableObject
{
    public List<SkillNodeData> nodes;
}