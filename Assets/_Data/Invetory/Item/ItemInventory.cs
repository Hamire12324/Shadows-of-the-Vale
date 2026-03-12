using System;
using UnityEngine;

[Serializable]
public class ItemInventory
{
    [SerializeField] private ItemProfileSO itemProfileSO;
    public ItemProfileSO ItemProfileSO => itemProfileSO;

    [SerializeField] private int itemCount;
    public int ItemCount => itemCount;

    public bool IsEmpty => itemProfileSO == null || itemCount <= 0;
    public bool IsStackable => itemProfileSO != null && itemProfileSO.IsStackable;
    public int MaxStack => itemProfileSO != null ? itemProfileSO.MaxStack : 0;

    public ItemInventory()
    {
        Clear();
    }
    public ItemInventory(ItemProfileSO itemProfileSO, int count)
    {
        Set(itemProfileSO, count);
    }
    public void Set(ItemProfileSO itemProfileSO, int count)
    {
        this.itemProfileSO = itemProfileSO;
        this.itemCount = count;
    }
    public void Clear()
    {
        itemProfileSO = null;
        itemCount = 0;
    }
    public bool CanAdd(int amount, out int added)
    {
        added = AddQuantity(amount);
        return added > 0;
    }
    public bool CanRemove(int amount)
    {
        return itemCount >= amount;
    }
    public int AddQuantity(int amount)
    {
        if (!IsStackable || amount <= 0)
            return 0;

        int spaceLeft = MaxStack - itemCount;
        int addAmount = Mathf.Min(spaceLeft, amount);

        itemCount += addAmount;
        return addAmount;
    }
    public bool RemoveQuantity(int amount)
    {
        if (!CanRemove(amount))
            return false;

        itemCount -= amount;

        if (itemCount <= 0)
            Clear();

        return true;
    }
    public ItemInventory Split(int amount)
    {
        if (amount <= 0 || amount >= itemCount)
            return null;

        itemCount -= amount;
        return new ItemInventory(itemProfileSO, amount);
    }
    public bool IsSame(ItemInventory other)
    {
        if (other == null || other.itemProfileSO == null)
            return false;

        return other.itemProfileSO == itemProfileSO;
    }
    public bool CanStack(int amount)
    {
        if (ItemProfileSO == null) return false;
        if (!ItemProfileSO.IsStackable) return false;

        return ItemCount + amount <= ItemProfileSO.MaxStack;
    }
    public ItemInventory Clone()
    {
        return new ItemInventory(itemProfileSO, itemCount);
    }
}