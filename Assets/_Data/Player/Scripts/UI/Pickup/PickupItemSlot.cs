using UnityEngine;
public class PickupItemSlot : BaseMonoBehaviour
{
    public PickupImage pickupImage;
    public PickupTxt pickupTxt;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadIcon();
        this.LoadNameText();
    }
    protected virtual void LoadIcon()
    {
        if (pickupImage != null) return;
        pickupImage = GetComponentInChildren<PickupImage>();
    }
    protected virtual void LoadNameText()
    {
        if (pickupTxt != null) return;
        pickupTxt = GetComponentInChildren<PickupTxt>();
    }
    public void Setup(ItemPickupTrigger item)
    {
        var drop = item.ItemDropCtrl;

        pickupImage?.SetSprite(drop.Icon);
        pickupTxt?.SetText(drop.ItemName);
    }
    public void SetHighlight(bool isSelected)
    {
        if (isSelected)
            pickupTxt.SetColor(Color.yellow);
        else
            pickupTxt.SetColor(Color.white);
    }
}