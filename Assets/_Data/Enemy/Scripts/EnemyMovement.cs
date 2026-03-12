using UnityEngine;
public class EnemyMovement : BaseMonoBehaviour
{
    [SerializeField] private EnemyCtrl enemyCtrl;

    //[SerializeField] private float walkSpeed = 0.5f;
    [SerializeField] private float runSpeed = 1f;

    [SerializeField] private float attackStopDistance = 2f;
    [SerializeField] private PlayerCtrl target;
    public enum State { Idle, Chase, Attack }
    [SerializeField] private State state = State.Idle;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (enemyCtrl == null) enemyCtrl = GetComponentInParent<EnemyCtrl>();
    }

    protected override void Update()
    {
        if (enemyCtrl.EnemyDamReceiver.isDead)
        {
            if (enemyCtrl.Agent.isOnNavMesh)
                enemyCtrl.Agent.isStopped = true;

            enemyCtrl.Animator.SetFloat("Speed", 0);
            return;
        }
        target = enemyCtrl.EnemyVision.GetTarget();

        enemyCtrl.EnemyAwareness.UpdateSuspicion();
        enemyCtrl.EnemyAwareness.UpdateVisionAngle();

        if (target != null)
            LookAtTarget();

        if (!enemyCtrl.Agent.isOnNavMesh) return;

        if (target == null)
        {
            state = State.Idle;
            enemyCtrl.Agent.isStopped = true;
        }
        else
        {
            float dist = Vector3.Distance(transform.position, target.transform.position);
            state = (dist > attackStopDistance) ? State.Chase : State.Attack;
        }

        RunState();
        UpdateAnimatorSpeed();
    }
    private void RunState()
    {
        if (!enemyCtrl.Agent.isOnNavMesh) return;

        switch (state)
        {
            case State.Idle:
                enemyCtrl.Agent.isStopped = true;
                break;

            case State.Chase:
                enemyCtrl.Agent.isStopped = false;
                enemyCtrl.Agent.SetDestination(target.transform.position);
                break;

            case State.Attack:
                enemyCtrl.Agent.isStopped = true;
                LookAtTarget();
                enemyCtrl.EnemyAttackController.TriggerAttack();
                break;
        }
    }

    private void LookAtTarget()
    {
        if (target == null) return;

        Vector3 dir = target.transform.position - transform.position;
        dir.y = 0;

        if (dir.sqrMagnitude > 0.001f)
            transform.parent.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(dir),
                10f * Time.deltaTime);
    }
    private void UpdateAnimatorSpeed()
    {
        float agentSpeed = enemyCtrl.Agent.velocity.magnitude;

        float normalizedSpeed = Mathf.Clamp(agentSpeed / runSpeed, 0f, 1f);

        enemyCtrl.Animator.SetFloat("Speed", normalizedSpeed, 0.1f, Time.deltaTime);
    }
}
