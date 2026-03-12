using UnityEngine;

public abstract class BaseShowHideUI : BaseMonoBehaviour, IShowHideUI
{
    [SerializeField] protected Transform showHide;

    protected virtual void OnShow()
    {
        PlayerManager.playerCtrl.PlayerCamera.LockCamera(true);
        PlayerManager.playerCtrl.PlayerLockHandler.LockPlayer();
    }

    protected virtual void OnHide()
    {
        PlayerManager.playerCtrl.PlayerCamera.LockCamera(false);
        PlayerManager.playerCtrl.PlayerLockHandler.UnlockPlayer();
    }
    public bool IsShow => this.showHide != null && this.showHide.gameObject.activeSelf;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadShowHide();
    }

    private void LoadShowHide()
    {
        if (this.showHide != null) return;

        this.showHide = transform.Find("ShowHide");

        if (this.showHide == null)
            Debug.LogError($"{name}: ShowHide not found");

    }

    public virtual void Show()
    {
        if (this.showHide == null) return;

        this.showHide.gameObject.SetActive(true);

        this.OnShow();
    }

    public virtual void Hide()
    {
        if (this.showHide == null) return;

        this.showHide.gameObject.SetActive(false);

        this.OnHide();
    }

    public virtual void Toggle()
    {
        if (IsShow)
            this.Hide();
        else
            this.Show();
    }
}