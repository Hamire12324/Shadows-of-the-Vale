using UnityEngine;

public class HUDManager: BaseSingleton<HUDManager>
{
    [Header("HUD References")]
    [SerializeField] private StaminaUI staminaUI;

    private PlayerCtrl localPlayer;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIReferences();
    }

    protected virtual void LoadUIReferences()
    {
        if (this.staminaUI != null) return;
        this.staminaUI = FindAnyObjectByType<StaminaUI>(FindObjectsInactive.Include);
        Debug.Log(transform.name + ": LoadUIReferences", gameObject);
    }

    public void RegisterLocalPlayer(PlayerCtrl player)
    {
        if (player == null) return;

        localPlayer = player;

        staminaUI?.SetPlayer(player);
    }

    public PlayerCtrl GetLocalPlayer() => localPlayer;
}
