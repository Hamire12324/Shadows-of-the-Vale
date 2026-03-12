using UnityEngine;

public class EnemyAwareness : BaseMonoBehaviour
{
    [SerializeField] private EnemyCtrl enemyCtrl;

    [Header("Suspicion Settings")]
    [SerializeField] private float suspicion = 0f;
    [SerializeField] private float suspicionIncreaseRate = 0.2f;
    [SerializeField] private float suspicionDecreaseRate = 0.1f;
    [SerializeField] private float suspicionHoldTime = 7f;
    [SerializeField] private float suspicionTimer = 0f;

    [Header("View Angle Settings")]
    [SerializeField] private float baseViewAngle = 120f;
    [SerializeField] private float maxViewAngle = 360f;

    protected override void Update()
    {
        UpdateSuspicion();
        UpdateVisionAngle();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (enemyCtrl == null) enemyCtrl = GetComponentInParent<EnemyCtrl>();
    }
    public void AddSuspicion(float amount)
    {
        suspicion = Mathf.Clamp01(suspicion + amount);
        suspicionTimer = suspicionHoldTime;
    }

    public void UpdateSuspicion()
    {
        if (enemyCtrl.EnemyVision.GetTarget() != null)
        {
            suspicion += suspicionIncreaseRate * Time.deltaTime;
            suspicionTimer = suspicionHoldTime;
        }
        else
        {
            if (suspicionTimer > 0)
                suspicionTimer -= Time.deltaTime;
            else
                suspicion -= suspicionDecreaseRate * Time.deltaTime;
        }

        suspicion = Mathf.Clamp01(suspicion);
    }

    public void UpdateVisionAngle()
    {
        float angle = Mathf.Lerp(baseViewAngle, maxViewAngle, suspicion);
        enemyCtrl.EnemyVision.SetViewAngle(angle);
    }
}
