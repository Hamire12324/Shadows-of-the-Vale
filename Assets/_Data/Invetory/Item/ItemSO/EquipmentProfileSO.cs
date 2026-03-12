using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Items/Equipment")]
public class EquipmentProfileSO : ItemProfileSO
{
    [SerializeField] private string setID;
    public string SetID => setID;
    public EquipSlotType equipSlotType;
    public List<StatModifierData> statBonuses;
}