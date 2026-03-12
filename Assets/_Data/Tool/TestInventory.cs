using UnityEngine;
using UnityEngine.InputSystem;

public class TestInventory : MonoBehaviour
{
    [Header("Item Test")]
    [SerializeField] private ItemProfileSO itemProfileSO;

    [SerializeField] private int itemCount = 1;

    [Header("Slot Remove Test")]
    [SerializeField] private int slotIndex = 0;

    PlayerInventoryManager Inventory =>
        PlayerManager.playerCtrl.PlayerInventoryManager;

    private void Update()
    {
        if (Keyboard.current.uKey.wasPressedThisFrame)
        {
            AddItem();
        }

        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            RemoveItem();
        }

        if (Keyboard.current.deleteKey.wasPressedThisFrame)
        {
            RemoveItemAtSlot();
        }
    }

    [ContextMenu("Add Item")]
    public void AddItem()
    {
        Inventory.AddItem(itemProfileSO, itemCount);

        Debug.Log($"ADD: {itemProfileSO} x{itemCount}");
    }

    [ContextMenu("Remove Item")]
    public void RemoveItem()
    {
        Inventory.RemoveItem(itemProfileSO, itemCount);

        Debug.Log($"REMOVE: {itemProfileSO} x{itemCount}");
    }

    [ContextMenu("Remove Item At Slot")]
    public void RemoveItemAtSlot()
    {
        Inventory.RemoveItemAt(slotIndex);
        Debug.Log($"REMOVE SLOT: {slotIndex}");
    }
}