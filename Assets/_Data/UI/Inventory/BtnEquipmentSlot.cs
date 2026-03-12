using UnityEngine;
using UnityEngine.EventSystems;

public class BtnEquipmentSlot : BtnInventorySlotBase, IDropHandler
{
    [SerializeField] private EquipSlotType equipType;
    public EquipSlotType EquipType => equipType;
    [SerializeField] private Sprite defaultSprite;

    protected override void OnClick()
    {
        inventoryUIController.OnEquipmentSlotClicked(equipType);
    }

    public void OnDrop(PointerEventData eventData)
    {
        var drag = eventData.pointerDrag?.GetComponent<InventoryDragHandler>();
        if (drag == null) return;

        if (!drag.IsFromEquipment)
        {
            // Bag → Equip
            inventoryUIController.HandleEquipDrop(drag.FromBagIndex, equipType);
        }
        else
        {
            // Equip → Equip
            if (drag.FromEquipType == equipType) return;

            inventoryUIController.HandleEquipmentSwap(drag.FromEquipType, equipType);
        }
    }
    protected override void RenderEmpty()
    {
        icon.enabled = true;
        icon.sprite = defaultSprite;
        icon.color = Color.white;
    }
}