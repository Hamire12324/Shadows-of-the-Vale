using UnityEngine;

public class StaminaUI : ImageUI
{
    [Header("References")]
    [SerializeField] private CanvasGroup canvasGroup;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCanvasGroup();
    }

    protected virtual void LoadCanvasGroup()
    {
        if (this.canvasGroup != null) return;
        this.canvasGroup = GetComponent<CanvasGroup>();
    }

    protected override void OnEnable()
    {
        PlayerCtrl.OnPlayerReady += RegisterInput;

        if (PlayerManager.playerCtrl != null)
        {
            RegisterInput();
        }
    }

    protected override void OnDisable()
    {
        PlayerCtrl.OnPlayerReady -= RegisterInput;

        if (PlayerManager.playerCtrl != null)
            PlayerManager.playerCtrl.PlayerStamina.OnStaminaChanged -= UpdateStaminaUI;
    }

    protected virtual void RegisterInput()
    {
        if (PlayerManager.playerCtrl == null) return;

        PlayerManager.playerCtrl.PlayerStamina.OnStaminaChanged -= UpdateStaminaUI;
        PlayerManager.playerCtrl.PlayerStamina.OnStaminaChanged += UpdateStaminaUI;

        UpdateStaminaUI(PlayerManager.playerCtrl.PlayerStamina.Normalized);
    }

    public void SetPlayer(PlayerCtrl player)
    {
        if (PlayerManager.playerCtrl != null) 
            PlayerManager.playerCtrl.PlayerStamina.OnStaminaChanged -= UpdateStaminaUI;

        PlayerManager.playerCtrl = player;

        if (PlayerManager.playerCtrl == null) return;

        PlayerManager.playerCtrl.PlayerStamina.OnStaminaChanged += UpdateStaminaUI;
        UpdateStaminaUI(PlayerManager.playerCtrl.PlayerStamina.Normalized);
    }

    private void UpdateStaminaUI(float normalizedValue)
    {
        image.fillAmount = normalizedValue;

        bool isFull = normalizedValue >= 0.99f;
        SetVisible(!isFull);
    }

    private void SetVisible(bool visible)
    {
        canvasGroup.alpha = visible ? 1f : 0f;
        canvasGroup.interactable = visible;
        canvasGroup.blocksRaycasts = visible;
    }
}
