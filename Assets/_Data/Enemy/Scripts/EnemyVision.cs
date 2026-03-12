using UnityEngine;

public class EnemyVision : TargetFinderBase
{
    [SerializeField] float viewAngle = 120f;
    [SerializeField] LayerMask obstacleMask;
    [SerializeField] float eyeHeight = 1.5f;
    protected override void ResetValue()
    {
        base.ResetValue();
        obstacleMask = LayerMask.GetMask("Player");
    }
    public bool IsVisionForced { get; private set; }

    float forceTimer;

    protected override void Update()
    {
        if (!IsVisionForced) return;

        forceTimer -= Time.deltaTime;

        if (forceTimer <= 0) IsVisionForced = false;
    }
    protected override void Reset()
    {
        base.Reset();
        targetMask = LayerMask.GetMask("Player");
    }
    public void ForceVision(float duration)
    {
        IsVisionForced = true;
        forceTimer = duration;
    }

    public PlayerCtrl GetTarget()
    {
        int count = FindTargets();

        PlayerCtrl best = null;
        float bestDist = Mathf.Infinity;

        float currentAngle = IsVisionForced ? 360f : viewAngle;

        for (int i = 0; i < count; i++)
        {
            Transform t = buffer[i].transform;

            Vector3 dir = (t.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dir) > currentAngle * 0.5f)
                continue;

            float dist = Vector3.Distance(transform.position, t.position);

            if (Physics.Raycast(
                transform.position + Vector3.up * eyeHeight,
                dir,
                out RaycastHit hit,
                dist,
                obstacleMask))
            {
                if (hit.transform != t)
                    continue;
            }

            var player = t.GetComponentInParent<PlayerCtrl>();

            if (player == null) continue;

            if (dist < bestDist)
            {
                bestDist = dist;
                best = player;
            }
        }
        return best;
    }
    public void SetViewAngle(float angle)
    {
        viewAngle = angle;
    }
}