using TMPro;
using UnityEngine;

public class SkillPointUI : TxtAbstract
{
    protected override void Start()
    {
        base.Start();

        PlayerManager.playerCtrl.CharacterLevel.OnSkillPointChanged += RefreshUI;

        RefreshUI(PlayerManager.playerCtrl.CharacterLevel.SkillPoints);
    }

    void RefreshUI(int points)
    {
        textMeshPro.text = "Skill Points: " + points;
    }

    protected override void OnDestroy()
    {
        if (PlayerManager.playerCtrl.CharacterLevel != null)
            PlayerManager.playerCtrl.CharacterLevel.OnSkillPointChanged -= RefreshUI;
    }
}