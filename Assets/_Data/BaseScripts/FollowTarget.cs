using UnityEngine;

public class FollowTarget : BaseMonoBehaviour
{
    [SerializeField] protected Transform target;
    Vector3 followOffset;
    Quaternion rotationOffset;

    public void SetTarget(Transform t)
    {
        target = t;

        if (target != null)
        {
            followOffset = transform.parent.position - target.position;
            rotationOffset = Quaternion.Inverse(target.rotation) * transform.parent.rotation;

        }
    }

    protected override void LateUpdate()
    {
        if (target == null) return;

        transform.parent.position = target.position + followOffset;

        transform.parent.rotation = target.rotation * rotationOffset;
    }
}