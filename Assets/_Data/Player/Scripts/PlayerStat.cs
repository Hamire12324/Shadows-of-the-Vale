using UnityEngine;
public class PlayerStat : CharacterStat
{
    [Header("Player Only")]
    public StatValue MaxMP;
    public StatValue Stamina;

    protected override void ClearAllModifiers()
    {
        base.ClearAllModifiers();

        MaxMP?.ClearModifiers();
        Stamina?.ClearModifiers();
    }

    public override StatValue GetStat(StatType type)
    {
        if (type == StatType.MaxMP) return MaxMP;
        if (type == StatType.Stamina) return Stamina;

        return base.GetStat(type);
    }
}