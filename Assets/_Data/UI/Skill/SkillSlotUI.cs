using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillSlotUI : BaseMonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text keyText;
    [SerializeField] private Image cooldownOverlay;
    [SerializeField] private SkillRuntime skillRuntime;
    [SerializeField] private Sprite defaultIcon;
    [SerializeField] private TextMeshProUGUI manaCostText;
    protected override void Update()
    {
        if (skillRuntime == null) return;
        if (cooldownOverlay == null) return;

        float cd = skillRuntime.SkillCooldown.GetCooldownNormalized();
        cooldownOverlay.fillAmount = cd;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadIcon();
        this.LoadKeyText();
        this.LoadCooldownOverlay();
        this.LoadManaCostText();
    }
    protected virtual void LoadIcon()
    {
        if (icon != null) return;
        icon = transform.Find("Icon").GetComponent<Image>();
    }
    protected virtual void LoadKeyText()
    {
        if (keyText != null) return;
        keyText = GetComponentInChildren<TMP_Text>();
    }
    protected virtual void LoadCooldownOverlay()
    {
        if (cooldownOverlay != null) return;
        cooldownOverlay = transform.Find("CooldownOverlay").GetComponent<Image>();
    }
    protected virtual void LoadManaCostText()
    {
        if (manaCostText != null) return;
        manaCostText = transform.Find("ManaCostText").GetComponent<TextMeshProUGUI>();
    }
    public void Setup(SkillRuntime skillInstance, int index)
    {
        this.skillRuntime = skillInstance;

        icon.sprite = this.skillRuntime.SkillData.icon;
        keyText.text = (index + 1).ToString();

        float cost = skillRuntime.SkillResourceCost.GetFinalCost(skillRuntime);
        manaCostText.text = cost.ToString("0");

        skillRuntime.OnRuntimeChanged += RefreshUI;

    }
    void RefreshUI()
    {
        if (skillRuntime == null) return;

        float cost = skillRuntime.SkillResourceCost.GetFinalCost(skillRuntime);
        manaCostText.text = cost.ToString("0");
    }
    public void Clear()
    {
        if (skillRuntime != null)
            skillRuntime.OnRuntimeChanged -= RefreshUI;

        skillRuntime = null;

        icon.sprite = defaultIcon;
        icon.color = Color.white;

        keyText.text = "";

        cooldownOverlay.fillAmount = 0;
        manaCostText.text = "";
    }
}