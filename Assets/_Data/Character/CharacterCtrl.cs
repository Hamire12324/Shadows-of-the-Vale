using Unity.VisualScripting;
using UnityEngine;

public abstract class CharacterCtrl : PoolObj

{
    [Header("Character Info")]
    [SerializeField] public Faction faction;
    [SerializeField] private CharacterController characterController;
    public CharacterController CharacterController => characterController;
    [SerializeField] private Transform model;
    public Transform Model => model;
    [SerializeField] private Animator animator;
    public Animator Animator => animator;
    [SerializeField] private StatusEffectController statusEffect;
    public StatusEffectController StatusEffect => statusEffect;
    [SerializeField] private WeaponManager weaponManager;
    public WeaponManager WeaponManager => weaponManager;
    [SerializeField] private CharacterWeaponHolder characterWeaponHolder;
    public CharacterWeaponHolder CharacterWeaponHolder => characterWeaponHolder;
    [SerializeField] private DamReceiverBase damReceiverBase;
    public DamReceiverBase DamReceiverBase => damReceiverBase;
    [SerializeField] protected CharacterStat characterStat;
    public CharacterStat CharacterStat => characterStat;
    [SerializeField] protected CharacterCombo characterCombo;
    public CharacterCombo CharacterCombo => characterCombo;
    [SerializeField] protected CharacterAttackState characterAttackState;
    public CharacterAttackState CharacterAttackState => characterAttackState;
    [SerializeField] protected CharacterAttackMotion characterAttackMotion;
    public CharacterAttackMotion CharacterAttackMotion => characterAttackMotion;
    [SerializeField] protected CharacterState characterState;
    public CharacterState CharacterState => characterState;
    [SerializeField] protected CharacterLevel characterLevel;
    public CharacterLevel CharacterLevel => characterLevel;
    [SerializeField] protected SkillManager skillManager;
    public SkillManager SkillManager => skillManager;
    public PlayerStat PlayerStat => characterStat as PlayerStat;
    protected abstract void GetFaction();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCharacterController();
        this.LoadModel();
        this.LoadAnimator();
        this.LoadStatusEffectController();
        this.LoadWeaponManager();
        this.LoadCharacterWeaponHolder();
        this.LoadDamReceiverBase();
        this.LoadCharacterStat();
        this.LoadCharacterCombo();
        this.LoadCharacterAttackState();
        this.LoadCharacterAttackMotion();
        this.LoadCharacterState();
        this.LoadCharacterLevel();
        this.LoadSkillManager();
    }
    protected virtual void LoadCharacterController()
    {
        if (this.characterController != null) return;
        this.characterController = GetComponent<CharacterController>();
        Debug.Log(transform.name + ": LoadCharacterController", gameObject);
    }
    protected virtual void LoadModel()
    {
        if (this.model != null) return;
        this.model = transform.Find("Model");
        Debug.Log(transform.name + ": LoadModel", gameObject);
    }
    protected virtual void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = GetComponentInChildren<Animator>();
        Debug.Log(transform.name + ": LoadAnimator", gameObject);
    }
    protected virtual void LoadStatusEffectController()
    {
        if (this.statusEffect != null) return;
        this.statusEffect = GetComponentInChildren<StatusEffectController>();
        Debug.Log(transform.name + ": LoadStatusEffectController", gameObject);
    }
    protected virtual void LoadWeaponManager()
    {
        if (this.weaponManager != null) return;
        this.weaponManager = GetComponentInChildren<WeaponManager>();
        Debug.Log(transform.name + ": LoadWeaponManager", gameObject);
    }
    protected virtual void LoadCharacterWeaponHolder()
    {
        if (this.characterWeaponHolder != null) return;
        this.characterWeaponHolder = GetComponentInChildren<CharacterWeaponHolder>(true);
        Debug.Log(transform.name + ": LoadCharacterWeaponHolder", gameObject);
    }
    protected virtual void LoadDamReceiverBase()
    {
        if (this.damReceiverBase != null) return;
        this.damReceiverBase = GetComponentInChildren<DamReceiverBase>();
        Debug.Log(transform.name + ": DamReceiverBase", gameObject);
    }

    protected virtual void LoadCharacterStat()
    {
        if (this.characterStat != null) return;
        this.characterStat = GetComponentInChildren<CharacterStat>();
        Debug.Log(transform.name + ": LoadCharacterStat", gameObject);
    }
    protected virtual void LoadCharacterCombo()
    {
        if (this.characterCombo != null) return;
        this.characterCombo = GetComponentInChildren<CharacterCombo>();
        Debug.Log(transform.name + ": LoadCharacterCombo", gameObject);
    }
    protected virtual void LoadCharacterAttackState()
    {
        if (this.characterAttackState != null) return;
        this.characterAttackState = GetComponentInChildren<CharacterAttackState>();
        Debug.Log(transform.name + ": LoadCharacterAttackState", gameObject);
    }
    protected virtual void LoadCharacterAttackMotion()
    {
        if (this.characterAttackMotion != null) return;
        this.characterAttackMotion = GetComponentInChildren<CharacterAttackMotion>();
        Debug.Log(transform.name + ": LoadCharacterAttackMotion", gameObject);
    }
    protected virtual void LoadCharacterState()
    {
        if (this.characterState != null) return;
        this.characterState = GetComponentInChildren<CharacterState>();
        Debug.Log(transform.name + ": LoadCharacterState", gameObject);
    }
    protected virtual void LoadCharacterLevel()
    {
        if (this.characterLevel != null) return;
        this.characterLevel = GetComponentInChildren<CharacterLevel>();
        Debug.Log(transform.name + ": LoadCharacterLevel", gameObject);
    }
    protected virtual void LoadSkillManager()
    {
        if (this.skillManager != null) return;
        this.skillManager = GetComponentInChildren<SkillManager>();
        Debug.Log(transform.name + ": LoadSkillManager", gameObject);
    }

}
