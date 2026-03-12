using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Items/ItemProfileSO")]
public class ItemProfileSO : ScriptableObject
{
    [Header("Base Info")]
    [SerializeField] private string itemID;
    public string ItemID => itemID;

    [SerializeField] private string itemName;
    public string ItemName => itemName;

    [SerializeField] private Sprite icon;
    public Sprite Icon => icon;

    [Header("Stack Settings")]
    [SerializeField] private bool isStackable = true;
    public bool IsStackable => isStackable;

    [SerializeField] private int maxStack = 99;
    public int MaxStack => maxStack;

    [Header("World Settings")]
    [SerializeField] private float worldSize = 0.5f;
    public float WorldSize => worldSize;
    public virtual void Use(PlayerCtrl playerCtrl)
    {
        // Mặc định không làm gì
    }
}
