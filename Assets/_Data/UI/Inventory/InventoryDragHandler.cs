using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryDragHandler : BaseMonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private bool isFromEquipment;
    public bool IsFromEquipment => isFromEquipment;

    [SerializeField] private int fromBagIndex;
    public int FromBagIndex => fromBagIndex;

    [SerializeField] private EquipSlotType fromEquipType;
    public EquipSlotType FromEquipType => fromEquipType;
    [SerializeField] private BtnInventorySlotBase btnInventorySlotBase;
    [SerializeField] private Canvas rootCanvas;

    private GameObject dragIcon;
    private RectTransform dragRect;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCanvas();
        this.LoadBtnInventorySlotBase();
    }
    protected virtual void LoadCanvas()
    {
        if (rootCanvas != null) return;
        rootCanvas = GetComponentInParent<Canvas>().rootCanvas;
        if (rootCanvas == null)
            Debug.LogError($"{name}: Canvas component not found in parent.");
    }
    protected virtual void LoadBtnInventorySlotBase()
    {
        if (btnInventorySlotBase != null) return;
        btnInventorySlotBase = GetComponent<BtnInventorySlotBase>();
        if (btnInventorySlotBase == null)
            Debug.LogError($"{name}: BtnInventorySlotBase component not found.");
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        var item = btnInventorySlotBase.ItemInventory;
        if (item == null || item.IsEmpty) return;

        var equipSlot = btnInventorySlotBase as BtnEquipmentSlot;
        if (equipSlot != null)
        {
            isFromEquipment = true;
            fromEquipType = equipSlot.EquipType;
        }
        else
        {
            isFromEquipment = false;
            fromBagIndex = btnInventorySlotBase.SlotIndex;
        }

        dragIcon = new GameObject("DragIcon");
        dragIcon.transform.SetParent(rootCanvas.transform, false);
        dragIcon.transform.SetAsLastSibling();

        var iconImage = dragIcon.AddComponent<UnityEngine.UI.Image>();
        iconImage.sprite = item.ItemProfileSO.Icon;
        iconImage.raycastTarget = false;

        dragRect = dragIcon.GetComponent<RectTransform>();
        dragRect.sizeDelta = new Vector2(64, 64);

        UpdateDragPosition(eventData);
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (dragRect != null)
            UpdateDragPosition(eventData);
    }

    private void UpdateDragPosition(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rootCanvas.transform as RectTransform,
            eventData.position,
            rootCanvas.worldCamera,
            out Vector2 localPoint);

        dragRect.localPosition = localPoint;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragIcon != null)
            Destroy(dragIcon);
    }
}