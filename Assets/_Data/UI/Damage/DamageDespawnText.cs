using UnityEngine;

public class DamageDespawnText : Despawn<DamageTextCtrl>
{
    protected override void ResetValue()
    {
        base.ResetValue();
        this.timeLife = 1;
        this.currentTime = 1;
    }
}