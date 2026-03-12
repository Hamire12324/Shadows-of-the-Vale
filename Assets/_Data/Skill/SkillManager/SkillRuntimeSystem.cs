using System.Collections.Generic;
using UnityEngine;
public class SkillRuntimeSystem
{
    private List<SkillRuntime> runtimeSkills = new();
    private Dictionary<SkillData, SkillRuntime> lookup = new();

    private SkillManager manager;

    Coroutine currentCastingRoutine;
    SkillRuntime currentRuntimeSkill;

    public System.Action<SkillRuntime> OnSkillAdded;
    public SkillRuntimeSystem(SkillManager manager)
    {
        this.manager = manager;
    }
    public void Load(List<SkillData> equippedSkills)
    {
        runtimeSkills.Clear();
        lookup.Clear();

        foreach (var data in equippedSkills)
        {
            SkillRuntime skill = new SkillRuntime(data, manager);

            runtimeSkills.Add(skill);
            lookup[data] = skill;

            OnSkillAdded?.Invoke(skill);
        }
    }
    public void AddSkill(SkillData data)
    {
        if (lookup.ContainsKey(data)) return;

        SkillRuntime skill = new SkillRuntime(data, manager);

        runtimeSkills.Add(skill);
        lookup[data] = skill;

        OnSkillAdded?.Invoke(skill);
    }
    public void RemoveSkill(SkillData data)
    {
        if (!lookup.TryGetValue(data, out var skill))
            return;

        runtimeSkills.Remove(skill);
        lookup.Remove(data);
    }
    public SkillRuntime GetRuntimeSkills(int index)
    {
        if (index < 0 || index >= runtimeSkills.Count)
            return null;

        return runtimeSkills[index];
    }
    public void CancelCasting()
    {
        manager.PlayerCtrl.CharacterState.SetCasting(false);

        if (currentRuntimeSkill != null)
        {
            DamageEffectExecutor.CancelEffect(manager.PlayerCtrl);
        }
        if (currentCastingRoutine != null)
        {
            manager.StopCoroutine(currentCastingRoutine);
            currentCastingRoutine = null;
        }

        if (manager.PlayerCtrl.Animator != null)
            manager.PlayerCtrl.Animator.SetBool("IsCasting", false);
    }
    public SkillRuntime GetSkillByIndex(SkillData data)
    {
        if (lookup.Count == 0) Debug.LogError("RuntimeSkills not loaded!");

        lookup.TryGetValue(data, out var skill);
        return skill;
    }
    public IReadOnlyList<SkillRuntime> GetAllRuntimeSkills()
    {
        return runtimeSkills;
    }
    public void ResetAllSkillModifiers()
    {
        foreach (var skill in runtimeSkills)
        {
            skill.SkillModifier.Reset();
        }
    }
    public void RegisterCastingRoutine(Coroutine routine)
    {
        currentCastingRoutine = routine;
    }
    public void RegisterCastingSkill(SkillRuntime skill)
    {
        currentRuntimeSkill = skill;
    }
}