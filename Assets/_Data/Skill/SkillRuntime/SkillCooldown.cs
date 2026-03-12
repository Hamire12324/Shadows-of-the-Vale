using UnityEngine;

public class SkillCooldown
{
    private float lastCastTime = -999f;
    private float cooldown;

    public SkillCooldown(float cooldown)
    {
        this.cooldown = cooldown;
    }

    public bool IsOnCooldown()
    {
        return Time.time < lastCastTime + cooldown;
    }

    public float GetRemaining()
    {
        Debug.Log(cooldown);
        return Mathf.Max(0f, (lastCastTime + cooldown) - Time.time);
    }

    public float GetCooldownNormalized()
    {
        if (cooldown <= 0) return 0;

        float remaining = GetRemaining();

        return Mathf.Clamp01(remaining / cooldown);
    }
    public void Trigger(float cooldown)
    {
        this.lastCastTime = Time.time;
        this.cooldown = cooldown;
    }
}