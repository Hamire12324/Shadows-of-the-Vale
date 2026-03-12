using UnityEngine;

public class PlayerTargetFinder : TargetFinderBase
{
    protected override void Reset()
    {
        base.ResetValue();
        this.targetMask = LayerMask.GetMask("Enemy");
    }
    public Transform FindNearest()
    {
        return FindNearestTransform();
    }
}