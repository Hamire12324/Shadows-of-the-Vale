using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentInventory : InventoryContainerBase
{
    private Dictionary<EquipSlotType, ItemInventory> slots;
    protected override void Awake()
    {
        base.Awake();

        slots = new Dictionary<EquipSlotType, ItemInventory>();

        foreach (EquipSlotType type in System.Enum.GetValues(typeof(EquipSlotType)))
        {
            if (type == EquipSlotType.None)
                continue;

            slots[type] = new ItemInventory();
        }
    }
    public ItemInventory Equip(EquipSlotType equipSlotType, ItemInventory itemInventory)
    {
        if (itemInventory == null || itemInventory.IsEmpty)
            return null;

        if (!slots.ContainsKey(equipSlotType))
            return null;

        if (!(itemInventory.ItemProfileSO is EquipmentProfileSO equipProfile))
            return null;

        if (equipProfile.equipSlotType != equipSlotType)
            return null;

        ItemInventory previous = slots[equipSlotType];

        slots[equipSlotType] = itemInventory;

        RaiseChanged();

        return previous;
    }
    public ItemInventory Unequip(EquipSlotType slotType)
    {
        if (!slots.ContainsKey(slotType))
            return null;

        ItemInventory current = slots[slotType];

        if (current == null || current.IsEmpty)
            return null;

        slots[slotType] = new ItemInventory();

        RaiseChanged();

        return current;
    }
    public void Swap(EquipSlotType a, EquipSlotType b)
    {
        var temp = slots[a];
        slots[a] = slots[b];
        slots[b] = temp;
    }
    public bool HasItem(EquipSlotType slotType)
    {
        if (!slots.ContainsKey(slotType))
            return false;

        return !slots[slotType].IsEmpty;
    }

    public ItemInventory GetItem(EquipSlotType slotType)
    {
        if (!slots.ContainsKey(slotType))
            return null;

        return slots[slotType];
    }
    public void ClearSlot(EquipSlotType equipType)
    {
        if (!slots.ContainsKey(equipType))
            return;

        slots[equipType] = new ItemInventory();
        RaiseChanged();
    }
    public override bool AddItem(ItemProfileSO item, int amount)
    {
        return false;
    }

    public override bool RemoveItem(ItemProfileSO item, int amount)
    {
        return false;
    }

    public override ItemInventory GetSlot(int index)
    {
        return null;
    }
}