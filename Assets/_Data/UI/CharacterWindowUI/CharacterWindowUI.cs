using UnityEngine;

public class CharacterWindowUI : BaseShowHideUI
{
    [Header("Tabs")]
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject skillPanel;
    [SerializeField] private SkillNodePopupUI skillNodePopup;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadInventoryUI();
        this.LoadSkillUI();
        this.LoadSkillPopup();

    }
    protected virtual void LoadInventoryUI()
    {
        if (this.inventoryPanel != null) return;
        this.inventoryPanel = GameObject.Find("InventoryPanel");
        if (this.inventoryPanel == null)
            Debug.LogError($"{name}: InventoryUI not found");
    }
    protected virtual void LoadSkillUI()
    {
        if (this.skillPanel != null) return;
        this.skillPanel = GameObject.Find("SkillPanel");
        if (this.skillPanel == null)
            Debug.LogError($"{name}: SkillUI not found");
    }
    void LoadSkillPopup()
    {
        if (skillNodePopup != null) return;

        skillNodePopup = GetComponentInChildren<SkillNodePopupUI>(true);
    }
    protected override void OnShow()
    {
        base.OnShow();

        ShowInventory();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    protected override void OnHide()
    {
        base.OnHide();

        if (skillNodePopup != null)
            skillNodePopup.Hide();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowInventory()
    {
        inventoryPanel.SetActive(true);
        skillPanel.SetActive(false);
    }

    public void ShowSkill()
    {
        inventoryPanel.SetActive(false);
        skillPanel.SetActive(true);
    }
}