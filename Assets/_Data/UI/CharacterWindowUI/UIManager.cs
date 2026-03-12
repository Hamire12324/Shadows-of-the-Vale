using UnityEngine;

public class UIManager : BaseSingleton<UIManager>
{

    private IShowHideUI currentUI;
    public IShowHideUI CurrentUI => currentUI;

    [SerializeField] private PickupUI pickupUI;
    public PickupUI PickupUI => pickupUI;
    [SerializeField] private CharacterWindowUI characterWindowUI;
    public CharacterWindowUI CharacterWindowUI => characterWindowUI;
    protected override void OnEnable()
    {
        PlayerCtrl.OnPlayerReady += RegisterInput;
    }
    protected override void OnDisable()
    {
        PlayerManager.playerCtrl.PlayerInput.InventoryPressed -= ToggleInventory;
    }
    private void RegisterInput()
    {
        PlayerManager.playerCtrl.PlayerInput.InventoryPressed += ToggleInventory;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPickupUI();
        this.LoadCharacterWindow();
    }
    protected virtual void LoadPickupUI()
    {
        if (this.pickupUI != null) return;
        this.pickupUI = transform.GetComponentInChildren<PickupUI>();
        if (this.pickupUI == null) Debug.LogError($"{name}: PickupUI not found");
    }
    protected virtual void LoadCharacterWindow()
    {
        if (this.characterWindowUI != null) return;

        this.characterWindowUI = transform.GetComponentInChildren<CharacterWindowUI>();

        if (this.characterWindowUI == null)
            Debug.LogError($"{name}: CharacterWindowUI not found");
    }
    public void ShowUI(IShowHideUI newUI)
    {
        if (newUI == null) return;

        if (this.currentUI != null && this.currentUI != newUI) this.currentUI.Hide();

        if (newUI.IsShow)
        {
            newUI.Hide();
            this.currentUI = null;
        }
        else
        {
            newUI.Show();
            this.currentUI = newUI;
        }
    }

    public void CloseCurrentUI()
    {
        if (this.currentUI == null) return;

        this.currentUI.Hide();
        this.currentUI = null;

    }
    private void ToggleInventory()
    {
        this.ShowUI(this.characterWindowUI);
    }
}