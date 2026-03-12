using UnityEngine;
using UnityEngine.EventSystems;

public class BtnDropItem : ButtonAbstract, IDropHandler
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

        if (!drag.IsFromEquipment)
        {
            inventoryUIController.DropItemFromBag(drag.FromBagIndex);
        }
        else
        {
            inventoryUIController.DropItemFromEquipment(drag.FromEquipType);
        }
    }

    protected override void OnClick()
    {
    }
}