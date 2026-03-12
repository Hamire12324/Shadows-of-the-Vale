using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDamReceiver : DamReceiverBase
{
    [SerializeField] protected PlayerCtrl playerCtrl;
    protected override void ResetValue()
    {
        base.ResetValue();
        this.faction = Faction.Player;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerCtrl();
    }
    protected virtual void LoadPlayerCtrl()
    {
        if (playerCtrl != null) return;
        playerCtrl = GetComponentInParent<PlayerCtrl>();
        Debug.Log(transform.name + ": LoadPlayerCtrl", gameObject);
    }
    protected override void Die()
    {
        base.Die();
        playerCtrl.CharacterState.Die();
    }
}
