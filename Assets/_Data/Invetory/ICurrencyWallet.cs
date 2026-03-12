using System;
public interface ICurrencyWallet
{
    event Action OnCurrencyChanged;

    int GetAmount(CurrencyType type);
    void Add(CurrencyType type, int amount);
    bool Spend(CurrencyType type, int amount);
    bool HasEnough(CurrencyType type, int amount);
}