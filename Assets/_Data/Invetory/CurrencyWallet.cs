using System;
using System.Collections.Generic;

[Serializable]
public class CurrencyWallet : BaseMonoBehaviour, ICurrencyWallet
{
    private Dictionary<CurrencyType, int> balances = new();

    public event Action OnCurrencyChanged;

    public int GetAmount(CurrencyType type)
    {
        return balances.TryGetValue(type, out var value) ? value : 0;
    }

    public void Add(CurrencyType type, int amount)
    {
        if (amount <= 0) return;

        if (!balances.ContainsKey(type))
            balances[type] = 0;

        balances[type] += amount;
        OnCurrencyChanged?.Invoke();
    }

    public bool Spend(CurrencyType type, int amount)
    {
        if (!HasEnough(type, amount))
            return false;

        balances[type] -= amount;
        OnCurrencyChanged?.Invoke();
        return true;
    }

    public bool HasEnough(CurrencyType type, int amount)
    {
        return GetAmount(type) >= amount;
    }
}