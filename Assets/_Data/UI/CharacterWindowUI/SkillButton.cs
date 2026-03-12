using UnityEngine;

public class SkillButton : ButtonAbstract
{
    protected override void OnClick()
    {
        UIManager.Instance.CharacterWindowUI.ShowSkill();
    }
}
