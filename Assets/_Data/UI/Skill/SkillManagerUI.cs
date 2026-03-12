using System.Collections.Generic;
using UnityEngine;

public class SkillManagerUI : BaseMonoBehaviour
{
    [SerializeField] private List<SkillSlotUI> slots;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSlots();
    }
    protected override void OnEnable()
    {
        base.OnEnable();

        PlayerCtrl.OnPlayerReady += LoadSkills;

        if (PlayerManager.playerCtrl != null)
        {
            PlayerManager.playerCtrl
                .SkillManager
                .SkillRuntimeSystem
                .OnSkillAdded += OnSkillAdded;

            LoadSkills();
        }
    }
    protected override void OnDisable()
    {
        base.OnDisable();

        PlayerCtrl.OnPlayerReady -= LoadSkills;

        if (PlayerManager.playerCtrl != null)
        {
            PlayerManager.playerCtrl
                .SkillManager
                .SkillRuntimeSystem
                .OnSkillAdded -= OnSkillAdded;
        }
    }
    void OnSkillAdded(SkillRuntime skill)
    {
        LoadSkills();
    }
    protected virtual void LoadSlots()
    {
        if (slots.Count > 0) return;

        slots = new List<SkillSlotUI>(GetComponentsInChildren<SkillSlotUI>());

        Debug.Log("Loaded Skill Slots: " + slots.Count);
    }
    protected virtual void LoadSkills()
    {
        if (PlayerManager.playerCtrl == null) return;

        var skills = PlayerManager.playerCtrl
            .SkillManager
            .SkillRuntimeSystem
            .GetAllRuntimeSkills();

        for (int i = 0; i < slots.Count; i++)
        {
            if (i < skills.Count)
                slots[i].Setup(skills[i], i);
            else
                slots[i].Clear();
        }
    }
}