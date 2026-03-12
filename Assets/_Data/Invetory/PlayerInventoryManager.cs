using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerInventoryManager : PlayerAbstract
{
    [Header("Containers")]
    [SerializeField] private ItemInventoryContainer itemInventoryContainer;
    public ItemInventoryContainer ItemInventoryContainer => itemInventoryContainer;
    [SerializeField] private EquipmentInventory equipmentInventory;
    public EquipmentInventory EquipmentInventory => equipmentInventory;

    [SerializeField] private CurrencyWallet currencyWallet;
    public CurrencyWallet CurrencyWallet => currencyWallet;
    [SerializeField] private SetDatabaseSO setDatabase;

    public event Action OnInventoryChanged;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItemInventoryContainer();
        this.LoadEquipmentInventory();
        this.LoadCurrencyWallet();

        equipmentInventory.OnChanged += RecalculateStats;
    }
    protected void LoadItemInventoryContainer()
    {
        if (itemInventoryContainer != null) return;
        itemInventoryContainer = GetComponentInChildren<ItemInventoryContainer>();
        if (itemInventoryContainer == null)
            Debug.LogError($"{name}: ItemInventoryContainer not found");
    }
    protected void LoadEquipmentInventory()
    {
        if (equipmentInventory != null) return;
        equipmentInventory = GetComponentInChildren<EquipmentInventory>();
        if (equipmentInventory == null)
            Debug.LogError($"{name}: EquipmentInventory not found");
    }
    protected void LoadCurrencyWallet()
    {
        if (currencyWallet != null) return;
        currencyWallet = GetComponentInChildren<CurrencyWallet>();
        if (currencyWallet == null)
            Debug.LogError($"{name}: CurrencyWallet not found");
    }
    public bool AddItem(ItemProfileSO profile, int amount)
    {
        if (!itemInventoryContainer.AddItem(profile, amount))
            return false;

        NotifyChanged();
        return true;
    }

    public bool RemoveItem(ItemProfileSO profile, int amount)
    {
        if (!itemInventoryContainer.RemoveItem(profile, amount))
            return false;

        NotifyChanged();
        return true;
    }
    public bool RemoveItemAt(int index)
    {
        var item = itemInventoryContainer.GetItem(index);

        if (item == null || item.IsEmpty)
            return false;

        itemInventoryContainer.ClearSlot(index);
        NotifyChanged();
        return true;
    }
    public void RemoveEquippedItem(EquipSlotType equipType)
    {
        equipmentInventory.ClearSlot(equipType);
        NotifyChanged();
    }
    public void SwapBagItem(int from, int to)
    {
        itemInventoryContainer.Swap(from, to);
        NotifyChanged();
    }
    public void SwapEquipment(EquipSlotType a, EquipSlotType b)
    {
        equipmentInventory.Swap(a, b);
        NotifyChanged();
    }
    public bool EquipItem(int bagIndex, EquipSlotType slotType)
    {
        ItemInventory item = itemInventoryContainer.GetSlot(bagIndex);
        if (item == null || item.IsEmpty)
            return false;

        ItemInventory previous = equipmentInventory.Equip(slotType, item);
        if (previous == null && !equipmentInventory.HasItem(slotType))
            return false;

        itemInventoryContainer.ClearSlot(bagIndex);

        if (previous != null && !previous.IsEmpty)
        {
            if (!itemInventoryContainer.AddItem(previous.ItemProfileSO, previous.ItemCount))
            {
                return false;
            }
        }

        NotifyChanged();
        return true;
    }
    public bool UnequipItem(EquipSlotType slotType)
    {
        ItemInventory item = equipmentInventory.GetItem(slotType);

        if (item == null || item.IsEmpty)
            return false;

        if (!itemInventoryContainer.CanAddItem(item.ItemProfileSO, item.ItemCount))
            return false;

        ItemInventory removed = equipmentInventory.Unequip(slotType);

        if (removed == null || removed.IsEmpty)
            return false;

        itemInventoryContainer.AddItem(removed.ItemProfileSO, removed.ItemCount);

        NotifyChanged();
        return true;
    }
    public void UnequipToSlot(EquipSlotType slotType, int bagIndex)
    {
        var item = equipmentInventory.GetItem(slotType);
        if (item == null || item.IsEmpty)
            return;

        if (!itemInventoryContainer.IsSlotEmpty(bagIndex))
            return;

        equipmentInventory.Unequip(slotType);
        itemInventoryContainer.SetSlot(bagIndex, item);

        NotifyChanged();
    }
    public void AddCurrency(CurrencyType type, int amount)
    {
        currencyWallet.Add(type, amount);
        NotifyChanged();
    }

    public bool SpendCurrency(CurrencyType type, int amount)
    {
        if (!currencyWallet.Spend(type, amount))
            return false;

        NotifyChanged();
        return true;
    }
    public void DropItemFromBag(int index)
    {
        var item = itemInventoryContainer.GetItem(index);
        if (item == null) return;

        ItemDropManager.Instance.Drop(item.ItemProfileSO, item.ItemCount, transform.position);

        itemInventoryContainer.ClearSlot(index);
        NotifyChanged();
    }
    public void DropItemFromEquipment(EquipSlotType type)
    {
        var item = equipmentInventory.GetItem(type);
        if (item == null) return;

        ItemDropManager.Instance.Drop(item.ItemProfileSO, item.ItemCount, transform.position);

        equipmentInventory.ClearSlot(type);
        NotifyChanged();
    }
    public void AddGold(int amount)
    {
        AddCurrency(CurrencyType.Gold, amount);
    }

    public bool SpendGold(int amount)
    {
        return SpendCurrency(CurrencyType.Gold, amount);
    }
    private void NotifyChanged()
    {
        OnInventoryChanged?.Invoke();
    }
    private void RecalculateStats()
    {
        List<StatModifier> allModifiers = new();

        Dictionary<string, int> setCounter = new();

        foreach (EquipSlotType type in System.Enum.GetValues(typeof(EquipSlotType)))
        {
            if (type == EquipSlotType.None) continue;

            var item = equipmentInventory.GetItem(type);

            if (item == null || item.IsEmpty) continue;

            if (item.ItemProfileSO is EquipmentProfileSO equipProfile)
            {
                foreach (var bonus in equipProfile.statBonuses)
                {
                    allModifiers.Add(bonus.ToRuntimeModifier());
                }

                if (!string.IsNullOrEmpty(equipProfile.SetID))
                {
                    if (!setCounter.ContainsKey(equipProfile.SetID))
                        setCounter[equipProfile.SetID] = 0;

                    setCounter[equipProfile.SetID]++;
                }
            }
        }

        foreach (var pair in setCounter)
        {
            string setID = pair.Key;
            int pieceCount = pair.Value;

            var setProfile = setDatabase.GetSet(setID);
            if (setProfile == null) continue;

            foreach (var bonus in setProfile.Bonuses)
            {
                if (pieceCount >= bonus.requiredPieces)
                {
                    foreach (var stat in bonus.statBonuses)
                    {
                        allModifiers.Add(stat.ToRuntimeModifier());
                    }
                }
            }
        }
        playerCtrl.CharacterStat.RecalculateAll(allModifiers);
        playerCtrl.DamReceiverBase.OnMaxHpChanged();
    }
}