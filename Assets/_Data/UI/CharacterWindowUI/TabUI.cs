using UnityEngine;

public class TabUI : BaseMonoBehaviour
{
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private SkillUI skillUI;

    public void ShowInventory()
    {
        inventoryUI.Show();
        skillUI.Hide();
    }

    public void ShowSkill()
    {
        inventoryUI.Hide();
        skillUI.Show();
    }
}
