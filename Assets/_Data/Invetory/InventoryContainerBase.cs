using System;

public abstract class InventoryContainerBase: BaseMonoBehaviour
{
    public event Action OnChanged;

    public abstract bool AddItem(ItemProfileSO itemProfileSO, int amount);
    public abstract bool RemoveItem(ItemProfileSO itemProfileSO, int amount);
    public abstract ItemInventory GetSlot(int index);

    protected void RaiseChanged()
    {
        OnChanged?.Invoke();
    }
}