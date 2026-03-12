using System.Linq;
using UnityEngine;

public class InventoryUIController : BaseMonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private PlayerInventoryManager playerInventoryManager;

    [Header("Bag UI")]
    [SerializeField] private BtnInventorySlot[] bagSlots;

    [Header("Equipment UI")]
    [SerializeField] private BtnEquipmentSlot[] equipmentSlots;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.playerInventoryManager = PlayerManager.playerCtrl.PlayerInventoryManager;
        this.LoadSlots();
    }
    private void LoadSlots()
    {
        playerInventoryManager = PlayerManager.playerCtrl.PlayerInventoryManager;

        bagSlots = GetComponentsInChildren<BtnInventorySlot>(true)
            .OrderBy(x => x.SlotIndex)
            .ToArray();

        equipmentSlots = GetComponentsInChildren<BtnEquipmentSlot>(true);
    }
    protected override void OnEnable()
    {
        if (playerInventoryManager != null)
            playerInventoryManager.OnInventoryChanged += RefreshAll;

        RefreshAll();
    }

    protected override void OnDisable()
    {
        if (playerInventoryManager != null)
            playerInventoryManager.OnInventoryChanged -= RefreshAll;
    }

    // ==============================
    // UI → Manager
    // ==============================

    public void HandleBagSwap(int fromIndex, int toIndex)
    {
        if (fromIndex == toIndex) return;
        playerInventoryManager.SwapBagItem(fromIndex, toIndex);
    }

    public void HandleEquipDrop(int fromBagIndex, EquipSlotType slotType)
    {
        playerInventoryManager.EquipItem(fromBagIndex, slotType);
    }

    public void HandleUnequipDrop(EquipSlotType fromEquip, int toBagIndex)
    {
        playerInventoryManager.UnequipToSlot(fromEquip, toBagIndex);
    }

    public void RemoveItem(int bagIndex)
    {
        playerInventoryManager.RemoveItemAt(bagIndex);
    }
    public void RemoveEquippedItem(EquipSlotType equipType)
    {
        playerInventoryManager.RemoveEquippedItem(equipType);
    }
    public void HandleEquipmentSwap(EquipSlotType from, EquipSlotType to)
    {
        playerInventoryManager.SwapEquipment(from, to);
    }
    public void DropItemFromBag(int bagIndex)
    {
        playerInventoryManager.DropItemFromBag(bagIndex);
    }

    public void DropItemFromEquipment(EquipSlotType equipType)
    {
        playerInventoryManager.DropItemFromEquipment(equipType);
    }
    // ==============================
    // Refresh UI
    // ==============================

    private void RefreshAll()
    {
        RefreshBag();
        RefreshEquipment();
    }

    private void RefreshBag()
    {
        var container = playerInventoryManager.ItemInventoryContainer;

        for (int i = 0; i < bagSlots.Length; i++)
        {
            var item = container.GetSlot(i);
            bagSlots[i].Render(item);
        }
    }

    private void RefreshEquipment()
    {
        var equip = playerInventoryManager.EquipmentInventory;

        foreach (var slot in equipmentSlots)
        {
            slot.Render(equip.GetItem(slot.EquipType));
        }
    }
    public void OnSlotClicked(int bagIndex)
    {
        //playerInventoryManager.RemoveItemAt(bagIndex);
    }
    public void OnEquipmentSlotClicked(EquipSlotType slotType)
    {
        //playerInventoryManager.UnequipItem(slotType);
    }
}