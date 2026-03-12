using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerPickup : PlayerAbstract
{
    [SerializeField] private List<ItemPickupTrigger> itemsInRange = new();
    public void TryPickup()
    {
        if (itemsInRange.Count == 0) return;

        var sorted = GetSortedItems();

        int index = UIManager.Instance.PickupUI.GetCurrentIndex();
        if (index < 0 || index >= sorted.Count) return;

        this.Pickup(sorted[index]);
    }

    private void Pickup(ItemPickupTrigger trigger)
    {
        ItemDropCtrl drop = trigger.ItemDropCtrl;

        bool added = playerCtrl.PlayerInventoryManager.AddItem(drop.ItemProfile, drop.ItemCount);

        if (!added) return;

        Unregister(trigger);
        trigger.Pickup();
    }

    private List<ItemPickupTrigger> GetSortedItems()
    {
        return itemsInRange
            .OrderBy(x => Vector3.Distance(transform.position, x.transform.position))
            .ToList();
    }

    public void Register(ItemPickupTrigger item)
    {
        if (item == null) return;

        if (!itemsInRange.Contains(item))
        {
            itemsInRange.Add(item);
            RefreshUI();
        }
    }

    public void Unregister(ItemPickupTrigger item)
    {
        if (itemsInRange.Remove(item))
            RefreshUI();
    }

    private void RefreshUI()
    {
        if (itemsInRange.Count == 0)
        {
            UIManager.Instance.PickupUI.Hide();
            return;
        }

        UIManager.Instance.PickupUI.Show(GetSortedItems(),
            UIManager.Instance.PickupUI.GetCurrentIndex());
    }
}