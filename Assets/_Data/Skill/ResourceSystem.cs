using System;
using UnityEngine;

[Serializable]
public class ResourceSystem
{
    [SerializeField] private float max;
    [SerializeField] private float current;

    public float Max => max;
    public float Current => current;

    public event Action<float, float> OnResourceChanged;

    public ResourceSystem(float maxValue)
    {
        max = maxValue;
        current = maxValue;
    }

    public bool HasEnough(float amount)
    {
        return current >= amount;
    }

    public void Add(float amount)
    {
        current = Mathf.Clamp(current + amount, 0, max);
        OnResourceChanged?.Invoke(current, max);
    }

    public void Spend(float amount)
    {
        current = Mathf.Clamp(current - amount, 0, max);
        OnResourceChanged?.Invoke(current, max);
    }

    public void SetMax(float newMax)
    {
        max = newMax;
        current = Mathf.Clamp(current, 0, max);
        OnResourceChanged?.Invoke(current, max);
    }
}