using System.Collections.Generic;
using UnityEngine;

public class ItemInventoryContainer : InventoryContainerBase
{
    [SerializeField] private int capacity = 50;
    [SerializeField] private List<ItemInventory> slots;
    protected override void ResetValue()
    {
        base.ResetValue();

        slots = new List<ItemInventory>();

        for (int i = 0; i < capacity; i++)
        {
            slots.Add(new ItemInventory());
        }
    }
    public override bool AddItem(ItemProfileSO item, int amount)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].IsEmpty)
            {
                slots[i] = new ItemInventory(item, amount);
                RaiseChanged();
                return true;
            }
        }

        return false;
    }

    public override bool RemoveItem(ItemProfileSO item, int amount)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (!slots[i].IsEmpty && slots[i].ItemProfileSO == item)
            {
                slots[i] = new ItemInventory();
                RaiseChanged();
                return true;
            }
        }

        return false;
    }
    public void Swap(int indexA, int indexB)
    {
        if (indexA < 0 || indexA >= slots.Count)
            return;

        if (indexB < 0 || indexB >= slots.Count)
            return;

        if (indexA == indexB)
            return;

        ItemInventory temp = slots[indexA];
        slots[indexA] = slots[indexB];
        slots[indexB] = temp;

        RaiseChanged();
    }
    public override ItemInventory GetSlot(int index)
    {
        return GetItem(index);
    }
    public ItemInventory GetItem(int index)
    {
        if (index < 0 || index >= slots.Count)
            return null;

        return slots[index];
    }
    public void SetSlot(int index, ItemInventory item)
    {
        if (index < 0 || index >= slots.Count)
            return;

        slots[index] = item ?? new ItemInventory();

        RaiseChanged();
    }
    public void ClearSlot(int index)
    {
        if (index < 0 || index >= slots.Count)
            return;

        slots[index] = new ItemInventory();
        RaiseChanged();
    }
    public bool CanAddItem(ItemProfileSO profile, int amount)
    {
        foreach (var slot in slots)
        {
            if (!slot.IsEmpty &&
                slot.ItemProfileSO == profile &&
                slot.CanStack(amount))
            {
                return true;
            }
        }

        foreach (var slot in slots)
        {
            if (slot.IsEmpty)
                return true;
        }

        return false;
    }
    public bool IsSlotEmpty(int index)
    {
        if (index < 0 || index >= slots.Count)
            return false;

        return slots[index] == null || slots[index].IsEmpty;
    }
}