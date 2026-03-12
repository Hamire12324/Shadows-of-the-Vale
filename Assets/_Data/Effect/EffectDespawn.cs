using UnityEngine;

public class EffectDespawn : Despawn<EffectCtrl>
{
    protected override void ResetValue()
    {
        base.ResetValue();
        this.timeLife = 5;
        this.currentTime = 5;
    }
}
