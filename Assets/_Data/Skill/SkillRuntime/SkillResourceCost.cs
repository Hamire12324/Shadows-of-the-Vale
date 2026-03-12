public class SkillResourceCost
{
    private ResourceType type;
    private float cost;

    public SkillResourceCost(ResourceType type, float cost)
    {
        this.type = type;
        this.cost = cost;
    }

    public bool CanPay(SkillManager manager, SkillRuntime skill)
    {
        if (type == ResourceType.None) return true;

        var resource = manager.ResourceManager.GetResource(type);

        if (resource == null) return false;

        float finalCost = cost + skill.SkillModifier.BonusCost;

        if (finalCost < 0) finalCost = 0;

        return resource.HasEnough(finalCost);
    }
    public void Pay(SkillManager manager, SkillRuntime skill)
    {
        if (type == ResourceType.None)
            return;

        var resource = manager.ResourceManager.GetResource(type);

        float finalCost = cost + skill.SkillModifier.BonusCost;

        if (finalCost < 0) finalCost = 0;

        resource?.Spend(finalCost);
    }
    public float GetFinalCost(SkillRuntime skill)
    {
        float finalCost = cost + skill.SkillModifier.BonusCost;

        if (finalCost < 0) finalCost = 0;

        return finalCost;
    }
}