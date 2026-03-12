using System.Collections.Generic;
using UnityEngine;

public class PickupUI : BaseMonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Transform contentParent;
    [SerializeField] private PickupItemSlot slotPrefab;

    private List<PickupItemSlot> currentSlots = new();
    [SerializeField] private int currentIndex = 0;
    [SerializeField] private bool isShowing;
    public bool IsShowing => isShowing;
    public void Show(List<ItemPickupTrigger> items, int selectedIndex)
    {
        panel.SetActive(true);
        isShowing = true;
        Clear();

        if (items.Count == 0)
        {
            currentIndex = 0;
        }
        else
        {
            currentIndex = Mathf.Clamp(selectedIndex, 0, items.Count - 1);
        }

        for (int i = 0; i < items.Count; i++)
        {
            PickupItemSlot slot = Instantiate(slotPrefab, contentParent);
            slot.Setup(items[i]);
            currentSlots.Add(slot);
        }
        UpdateHighlight();
    }

    public void ChangeSelection(int delta)
    {
        if (currentSlots.Count == 0) return;

        currentIndex += delta;

        if (currentIndex < 0)
            currentIndex = currentSlots.Count - 1;
        else if (currentIndex >= currentSlots.Count)
            currentIndex = 0;
        UpdateHighlight();
    }

    private void UpdateHighlight()
    {
        for (int i = 0; i < currentSlots.Count; i++)
        {
            currentSlots[i].SetHighlight(i == currentIndex);
        }
    }

    public int GetCurrentIndex() => currentIndex;

    public void Hide()
    {
        panel.SetActive(false);
        isShowing = false;
        Clear();
    }

    private void Clear()
    {
        foreach (var slot in currentSlots)
            Destroy(slot.gameObject);

        currentSlots.Clear();
    }
}