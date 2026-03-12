using UnityEngine;

public class AttackPoint : BaseMonoBehaviour
{
    protected override void ResetValue()
    {
        base.ResetValue();
        transform.localPosition = new Vector3(0.06f, 0.52f, 0);
    }
}
