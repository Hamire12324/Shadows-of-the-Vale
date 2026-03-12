using UnityEngine;

public abstract class TargetFinderBase : BaseMonoBehaviour
{
    [SerializeField] protected float radius = 5f;
    [SerializeField] protected LayerMask targetMask;

    protected Collider[] buffer = new Collider[20];
    protected Transform FindNearestTransform()
    {
        int count = FindTargets();

        Transform nearest = null;
        float minDist = Mathf.Infinity;

        for (int i = 0; i < count; i++)
        {
            Transform t = buffer[i].transform;

            float dist = Vector3.Distance(
                transform.position,
                t.position
            );

            if (dist < minDist)
            {
                minDist = dist;
                nearest = t;
            }
        }

        return nearest;
    }
    protected int FindTargets()
    {
        return Physics.OverlapSphereNonAlloc(
            transform.position,
            radius,
            buffer,
            targetMask
        );
    }

}