using UnityEngine;

public class ItemPickupTrigger : BaseMonoBehaviour
{
    [SerializeField] protected ItemDropCtrl itemDropCtrl;
    public ItemDropCtrl ItemDropCtrl => itemDropCtrl;


    private void OnTriggerEnter(Collider other)
    {
        PlayerPickup pickup = other.GetComponent<PlayerPickup>();
        if (pickup != null)
            pickup.Register(this);
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerPickup pickup = other.GetComponent<PlayerPickup>();
        if (pickup != null)
            pickup.Unregister(this);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItemDropCtrl();
    }

    protected virtual void LoadItemDropCtrl()
    {
        if (this.itemDropCtrl != null) return;
        this.itemDropCtrl = GetComponentInParent<ItemDropCtrl>();
    }

    public void Pickup()
    {
        itemDropCtrl.Despawn.DoDespawn();
    }
}