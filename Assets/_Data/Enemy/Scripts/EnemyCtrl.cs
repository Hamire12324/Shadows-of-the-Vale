using UnityEngine;
using UnityEngine.AI;

public class EnemyCtrl : CharacterCtrl
{
    [SerializeField] private EnemyVision enemyVision;
    public EnemyVision EnemyVision => enemyVision;
    [SerializeField] private EnemyDamReceiver enemyDamReceiver;
    public EnemyDamReceiver EnemyDamReceiver => enemyDamReceiver;
    [SerializeField] private NavMeshAgent agent;
    public NavMeshAgent Agent => agent;
    [SerializeField] private EnemyAwareness enemyAwareness;
    public EnemyAwareness EnemyAwareness => enemyAwareness;
    [SerializeField] private EnemyMovement enemyMovement;
    public EnemyMovement EnemyMovement => enemyMovement;
    [SerializeField] private EnemyAttackController enemyAttackController;
    public EnemyAttackController EnemyAttackController => enemyAttackController;
    protected override void ResetValue()
    {
        base.ResetValue();
        this.GetFaction();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        agent.enabled = true;
        this.EnemyDamReceiver.currentHp = this.EnemyDamReceiver.MaxHp;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadEnemyVision();
        this.LoadAgent();
        this.LoadEnemyAwareness();
        this.LoadEnemyMovement();
        this.LoadEnemyDamReceiver();
        this.LoadEnemyEnemyAttackController();
    }
    protected override void GetFaction()
    {
        this.faction = Faction.Enemy;
    }
    protected virtual void LoadEnemyVision()
    {
        if (this.enemyVision != null) return;
        this.enemyVision = GetComponentInChildren<EnemyVision>();
        Debug.Log(transform.name + ": LoadEnemyVision", gameObject);
    }
    protected virtual void LoadAgent()
    {
        if (this.agent != null) return;
        this.agent = GetComponent<NavMeshAgent>();
        Debug.Log(transform.name + ": LoadAgent", gameObject);
    }
    protected virtual void LoadEnemyAwareness()
    {
        if (this.enemyAwareness != null) return;
        this.enemyAwareness = GetComponentInChildren<EnemyAwareness>();
        Debug.Log(transform.name + ": LoadEnemyAwareness", gameObject);
    }
    protected virtual void LoadEnemyMovement()
    {
        if (this.enemyMovement != null) return;
        this.enemyMovement = GetComponentInChildren<EnemyMovement>();
        Debug.Log(transform.name + ": LoadEnemyMovement", gameObject);
    }
    protected virtual void LoadEnemyDamReceiver()
    {
        if (this.enemyDamReceiver != null) return;
        this.enemyDamReceiver = GetComponentInChildren<EnemyDamReceiver>();
        Debug.Log(transform.name + ": LoadEnemyDamReceiver", gameObject);
    }
    protected virtual void LoadEnemyEnemyAttackController()
    {
        if (this.enemyAttackController != null) return;
        this.enemyAttackController = GetComponentInChildren<EnemyAttackController>();
        Debug.Log(transform.name + ": LoadEnemyEnemyAttackController", gameObject);
    }
}
