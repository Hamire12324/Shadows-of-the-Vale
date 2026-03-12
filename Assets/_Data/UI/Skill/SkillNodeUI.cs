using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillNodeUI : ButtonAbstract
{
    [SerializeField] protected Image icon;
    [SerializeField] protected RectTransform rect;
    [SerializeField] protected GameObject lockIcon;

    private SkillNodeRuntime runtime;

    public Action<int> OnNodeClick;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRect();
        this.LoadIcon();
        this.LoadLockIcon();
    }

    protected void LoadRect()
    {
        if (rect == null)
            rect = GetComponent<RectTransform>();
    }
    protected void LoadIcon()
    {
        if (icon == null)
            icon = transform.Find("MaskCircle/Icon").GetComponent<Image>();
    }
    protected void LoadLockIcon()
    {
        if (lockIcon == null)
            lockIcon = transform.Find("MaskCircle/LockIcon").gameObject;
    }
    public void Init(SkillNodeRuntime nodeRuntime)
    {
        if (runtime != null)
            runtime.OnLevelChanged -= UpdateUI;

        runtime = nodeRuntime;

        icon.sprite = runtime.Data.icon;
        rect.anchoredPosition = runtime.Data.position;

        runtime.OnLevelChanged += UpdateUI;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClick);

        UpdateUI(runtime);
    }

    void UpdateUI(SkillNodeRuntime node)
    {
        Debug.Log("UpdateUI called: " + node.IsUnlocked);

        if (node.IsUnlocked)
        {
            icon.color = Color.white;
            lockIcon.SetActive(false);
        }
        else
        {
            icon.color = Color.gray;
            lockIcon.SetActive(true);
        }
    }

    protected override void OnClick()
    {
        if (runtime == null) return;

        OnNodeClick?.Invoke(runtime.Data.id);
    }
}