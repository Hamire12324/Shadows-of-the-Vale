public static class FactionManager
{
    public static bool CanAttack(Faction attacker, Faction target)
    {
        if (attacker == target) return false;
        if (attacker == Faction.Player && target == Faction.Ally) return false;
        return true;
    }
}