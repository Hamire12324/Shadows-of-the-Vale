using UnityEngine;
using UnityEngine.EventSystems;

public class BtnDeleteItem : ButtonAbstract, IDropHandler
{
    [SerializeField] private InventoryUIController inventoryUIController;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadInventoryUIController();
    }
    protected virtual void LoadInventoryUIController()
    {
        if (inventoryUIController != null) return;
        inventoryUIController = GetComponentInParent<InventoryUIController>(true);
    }

    public void OnDrop(PointerEventData eventData)
    {
        var drag = eventData.pointerDrag?.GetComponent<InventoryDragHandler>();
        if (drag == null)
            return;

        // Nếu kéo từ Bag
        if (!drag.IsFromEquipment)
        {
            inventoryUIController.RemoveItem(drag.FromBagIndex);
        }
        else
        {
            // Nếu kéo từ Equipment
            inventoryUIController.RemoveEquippedItem(drag.FromEquipType);
        }
    }
    protected override void OnClick()
    {

    }
}