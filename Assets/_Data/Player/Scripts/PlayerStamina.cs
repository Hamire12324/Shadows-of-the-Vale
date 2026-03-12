using System;
using UnityEngine;

public class PlayerStamina : PlayerAbstract
{
    [SerializeField] private float currentStamina;
    [SerializeField] private float regenRate = 15f;
    public float CurrentStamina => currentStamina;
    public float MaxStamina => playerCtrl.PlayerStat.Stamina.FinalValue;
    public float Normalized => currentStamina / MaxStamina;

    public event Action<float> OnStaminaChanged;
    protected override void Update()
    {
        Regenerate();
    }
    protected override void ResetValue()
    {
        base.ResetValue();
        this.currentStamina = this.MaxStamina;
    }
    private void Regenerate()
    {
        if (currentStamina >= MaxStamina) return;
        if (playerCtrl.PlayerDash.IsDashing) return;
        if (playerCtrl.PlayerMovement.IsRunning) return;
        currentStamina += regenRate * Time.deltaTime;
        currentStamina = Mathf.Clamp(currentStamina, 0, MaxStamina);
        OnStaminaChanged?.Invoke(Normalized);
    }

    public bool Consume(float amount)
    {
        if (currentStamina < amount) return false;

        currentStamina -= amount;
        OnStaminaChanged?.Invoke(Normalized);
        return true;
    }
    public void OnStatChanged()
    {
        currentStamina = Mathf.Clamp(currentStamina, 0, MaxStamina);
        OnStaminaChanged?.Invoke(Normalized);
    }
}
