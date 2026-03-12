using UnityEngine;

public class InventoryButton : ButtonAbstract
{
    protected override void OnClick()
    {
        UIManager.Instance.CharacterWindowUI.ShowInventory();
    }
}