using System;
using UnityEngine;

public class PlayerCtrl : CharacterCtrl
{
    [Header("Player Ctrl")]
    [SerializeField] private PlayerMovement playerMovement;
    public PlayerMovement PlayerMovement => playerMovement;
    [SerializeField] private Transform mainCamera;
    public Transform MainCamera => mainCamera;
    [SerializeField] protected PlayerInventoryManager playerInventoryManager;
    public PlayerInventoryManager PlayerInventoryManager => playerInventoryManager;
    [SerializeField] private PlayerStamina playerStamina;
    public PlayerStamina PlayerStamina => playerStamina;
    [SerializeField] private PlayerAnimation playerAnimation;
    public PlayerAnimation PlayerAnimation => playerAnimation;
    [SerializeField] private PlayerDash playerDash;
    public PlayerDash PlayerDash => playerDash;
    [SerializeField] private PlayerMotor playerMotor;
    public PlayerMotor PlayerMotor => playerMotor;
    [SerializeField] private PlayerAttackController playerAttackController;
    public PlayerAttackController PlayerAttackController => playerAttackController;
    [SerializeField] private PlayerAttackMotion playerAttackMotion;
    public PlayerAttackMotion PlayerAttackMotion => playerAttackMotion;
    [SerializeField] private PlayerInput playerInput;
    public PlayerInput PlayerInput => playerInput;
    [SerializeField] private PlayerTargetFinder playerTargetFinder;
    public PlayerTargetFinder PlayerTargetFinder => playerTargetFinder;
    [SerializeField] private PlayerLockHandler playerLockHandler;
    public PlayerLockHandler PlayerLockHandler => playerLockHandler;
    [SerializeField] private PlayerCamera playerCamera;
    public PlayerCamera PlayerCamera => playerCamera;
    [SerializeField] private PlayerPickup playerPickup;
    public PlayerPickup PlayerPickup => playerPickup;
    [SerializeField] private SkillTreeManager skillTreeManager;
    public SkillTreeManager SkillTreeManager => skillTreeManager;
    public PlayerAttackState PlayerAttackState => characterAttackState as PlayerAttackState;
    public static event Action OnPlayerReady;
    protected override void Start()
    {
        base.Start();
        OnPlayerReady?.Invoke();
    }
    protected override void ResetValue()
    {
        base.ResetValue();
        this.GetFaction();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();

        PlayerManager.playerCtrl = this;

        this.LoadPlayerMovement();
        this.LoadMainCamera();
        this.LoadPlayerStamina();
        this.LoadPlayerAnimation();
        this.LoadPlayerDash();
        this.LoadPlayerMotor();
        this.LoadPlayerAttackController();
        this.LoadPlayerAttackMotion();
        this.LoadPlayerInput();
        this.LoadPlayerTargetFinder();
        this.LoadPlayerLockHandler();
        this.LoadPlayerCamera();
        this.LoadPlayerInventoryManager();
        this.LoadPlayerPickUp();
        this.LoadSkillManager();
        this.LoadSkillTreeManager();
    }
    protected virtual void LoadPlayerMovement()
    {
        if (this.playerMovement != null) return;
        this.playerMovement = GetComponentInChildren<PlayerMovement>();
        Debug.Log(transform.name + ": LoadPlayerMovement", gameObject);
    }
    protected virtual void LoadMainCamera()
    {
        if (mainCamera != null) return;
        Camera cam = Camera.main;
        if (cam != null) mainCamera = cam.transform;
    }
    protected virtual void LoadPlayerStamina()
    {
        if (this.playerStamina != null) return;
        this.playerStamina = GetComponentInChildren<PlayerStamina>();
        Debug.Log(transform.name + ": LoadPlayerStamina", gameObject);
    }
    protected virtual void LoadPlayerAnimation()
    {
        if (this.playerAnimation != null) return;
        this.playerAnimation = GetComponentInChildren<PlayerAnimation>();
        Debug.Log(transform.name + ": LoadPlayerAnimation", gameObject);
    }
    protected virtual void LoadPlayerDash()
    {
        if (this.playerDash != null) return;
        this.playerDash = GetComponentInChildren<PlayerDash>();
        Debug.Log(transform.name + ": LoadPlayerDash", gameObject);
    }
    protected virtual void LoadPlayerMotor()
    {
        if (this.playerMotor != null) return;
        this.playerMotor = GetComponentInChildren<PlayerMotor>();
        Debug.Log(transform.name + ": LoadPlayerMotor", gameObject);
    }
    protected virtual void LoadPlayerAttackController()
    {
        if (this.playerAttackController != null) return;
        this.playerAttackController = GetComponentInChildren<PlayerAttackController>();
        Debug.Log(transform.name + ": LoadPlayerAttackController", gameObject);
    }
    protected virtual void LoadPlayerAttackMotion()
    {
        if (this.playerAttackMotion != null) return;
        this.playerAttackMotion = GetComponentInChildren<PlayerAttackMotion>();
        Debug.Log(transform.name + ": LoadPlayerAttackMotion", gameObject);
    }
    protected virtual void LoadPlayerInput()
    {
        if (this.playerInput != null) return;
        this.playerInput = GetComponentInChildren<PlayerInput>();
        Debug.Log(transform.name + ": LoadPlayerInput", gameObject);
    }
    protected virtual void LoadPlayerTargetFinder()
    {
        if (this.playerTargetFinder != null) return;
        this.playerTargetFinder = GetComponentInChildren<PlayerTargetFinder>();
        Debug.Log(transform.name + ": LoadPlayerTargetFinder", gameObject);
    }
    protected virtual void LoadPlayerLockHandler()
    {
        if (this.playerLockHandler != null) return;
        this.playerLockHandler = GetComponentInChildren<PlayerLockHandler>();
        Debug.Log(transform.name + ": LoadPlayerLockHandler", gameObject);
    }
    protected virtual void LoadPlayerCamera()
    {
        if (this.playerCamera != null) return;
        this.playerCamera = GetComponentInChildren<PlayerCamera>();
        Debug.Log(transform.name + ": LoadPlayerCamera", gameObject);
    }
    protected virtual void LoadPlayerInventoryManager()
    {
        if (this.playerInventoryManager != null) return;
        this.playerInventoryManager = GetComponentInChildren<PlayerInventoryManager>();
        Debug.Log(transform.name + ": LoadPlayerInventoryManager", gameObject);
    }
    protected virtual void LoadPlayerPickUp()
    {
        if (this.playerPickup != null) return;
        this.playerPickup = GetComponentInChildren<PlayerPickup>();
        Debug.Log(transform.name + ": LoadPlayerPickUp", gameObject);
    }
    protected virtual void LoadSkillTreeManager()
    {
        if (this.skillTreeManager != null) return;
        this.skillTreeManager = GetComponentInChildren<SkillTreeManager>();
        Debug.Log(transform.name + ": LoadSkillTreeManager", gameObject);
    }
    protected override void GetFaction()
    {
        this.faction = Faction.Player;
    }
}
