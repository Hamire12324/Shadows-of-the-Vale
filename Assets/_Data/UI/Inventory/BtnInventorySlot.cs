using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnInventorySlot : BtnInventorySlotBase, IDropHandler
{
    [SerializeField] private TextMeshProUGUI txtItemCount;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.txtItemCount = transform.Find("TxtItemCount").GetComponent<TextMeshProUGUI>();
    }
    public override void Render(ItemInventory itemInventory)
    {
        base.Render(itemInventory);

        bool showCount = itemInventory != null && itemInventory.ItemCount > 1;

        txtItemCount.gameObject.SetActive(showCount);

        if (showCount) txtItemCount.text = itemInventory.ItemCount.ToString();
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop called on: " + name);

        var drag = eventData.pointerDrag?.GetComponent<InventoryDragHandler>();
        if (drag == null) return;

        if (drag.IsFromEquipment)
        {
            // Equip → Bag
            inventoryUIController.HandleUnequipDrop(drag.FromEquipType, slotIndex);
        }
        else
        {
            // Bag → Bag
            if (drag.FromBagIndex == slotIndex) return;

            if (inventoryUIController == null)
            {
                Debug.LogError("inventoryUIController is NULL on " + name);
                return;
            }

            inventoryUIController.HandleBagSwap(drag.FromBagIndex, slotIndex);
        }
    }
}