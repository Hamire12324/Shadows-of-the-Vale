using UnityEngine;

public class StatusEffectController : BaseMonoBehaviour
{
    public bool IsStunned => stunTime > 0f;
    [SerializeField] private float stunTime = 0f;

    public void ApplyStun(float duration)
    {
        stunTime = duration;
    }

    protected override void Update()
    {
        if (stunTime > 0)
            stunTime -= Time.deltaTime;
    }
}
