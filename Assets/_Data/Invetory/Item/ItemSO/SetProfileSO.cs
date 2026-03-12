using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Items/SetProfileSO")]
public class SetProfileSO : ScriptableObject
{
    [SerializeField] private string setID;
    public string SetID => setID;

    [SerializeField] private List<SetBonus> bonuses;
    public List<SetBonus> Bonuses => bonuses;
}