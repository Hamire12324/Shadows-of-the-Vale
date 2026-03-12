using UnityEngine;
using UnityEngine.UI;

public abstract class BtnInventorySlotBase : ButtonAbstract
{
    [SerializeField] protected ItemInventory itemInventory;
    public ItemInventory ItemInventory => itemInventory;
    [SerializeField] protected CanvasGroup canvasGroup;
    [SerializeField] protected Image icon;
    public Image Icon => icon;
    [SerializeField] protected int slotIndex;
    public int SlotIndex => slotIndex;
    [SerializeField] protected InventoryUIController inventoryUIController;
    protected override void ResetValue()
    {
        base.ResetValue();

        slotIndex = transform.GetSiblingIndex();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
        if (icon == null)
            icon = transform.Find("Icon").GetComponent<Image>();

        this.LoadInventoryUIController();
    }
    protected virtual void LoadInventoryUIController()
    {
        if (inventoryUIController != null) return;
        inventoryUIController = GetComponentInParent<InventoryUIController>(true);
    }
    public void Init(int slotIndex, InventoryUIController inventoryUIController)
    {
        this.slotIndex = slotIndex;
        this.inventoryUIController = inventoryUIController;
    }
    public virtual void Render(ItemInventory item)
    {
        itemInventory = item;

        if (item == null || item.IsEmpty || item.ItemProfileSO == null)
        {
            RenderEmpty();
            return;
        }

        RenderItem(item);
    }
    protected virtual void RenderItem(ItemInventory item)
    {
        icon.enabled = true;
        icon.sprite = item.ItemProfileSO.Icon;
        icon.color = Color.white;
    }
    protected virtual void RenderEmpty()
    {
        icon.enabled = false;
    }

    public void SetSelected(bool value)
    {
        if (canvasGroup == null) return;
        canvasGroup.alpha = value ? 0.5f : 1f;
    }

    protected override void OnClick()
    {
        inventoryUIController.OnSlotClicked(slotIndex);
    }
}