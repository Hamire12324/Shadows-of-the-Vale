using UnityEngine.UI;
public interface IDraggableSlot
{
    int SlotIndex { get; }
    ItemInventory ItemInventory { get; }
    Image AccessoryIcon { get; }
    void SetItem(ItemInventory item);
    void SetAlpha(float alpha);
    void ClearSlot();
}
