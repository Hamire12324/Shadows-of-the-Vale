using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillNodePopupUI : BaseMonoBehaviour
{
    [Header("Root")]
    [SerializeField] private GameObject root;

    [Header("Header")]
    [SerializeField] private GameObject header;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI skillName;

    [Header("Unlock Panel")]
    [SerializeField] private GameObject unlockPanel;
    [SerializeField] private TextMeshProUGUI unlockCostText;
    [SerializeField] private Button unlockButton;
    [SerializeField] private TextMeshProUGUI requirementText;

    [Header("Detail Panel")]
    [SerializeField] private GameObject detailPanel;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI currentLevel;
    [SerializeField] private TextMeshProUGUI nextLevel;
    [SerializeField] private TextMeshProUGUI upgradeCostText;
    [SerializeField] private Button upgradeButton;

    private SkillNodeRuntime runtime;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        LoadRoot();
        LoadHeader();
        LoadUnlockPanel();
        LoadDetailPanel();
    }

    void LoadRoot()
    {
        if (root == null)
            root = transform.gameObject;
    }

    void LoadHeader()
    {
        if (header == null)
            header = transform.Find("Header").gameObject;

        if (icon == null)
            icon = transform.Find("Header/Icon/MaskCircle/Icon").GetComponent<Image>();

        if (skillName == null)
            skillName = transform.Find("Header/SkillName").GetComponent<TextMeshProUGUI>();
    }

    void LoadUnlockPanel()
    {
        if (unlockPanel == null)
            unlockPanel = transform.Find("UnlockPanel").gameObject;

        if (unlockCostText == null)
            unlockCostText = unlockPanel.transform
                .Find("CostText")
                .GetComponent<TextMeshProUGUI>();

        if (unlockButton == null)
            unlockButton = unlockPanel.transform
                .Find("UnlockButton")
                .GetComponent<Button>();
        if (requirementText == null)
            requirementText = unlockPanel.transform
                .Find("RequirementText")
                .GetComponent<TextMeshProUGUI>();
    }

    void LoadDetailPanel()
    {
        if (detailPanel == null)
            detailPanel = transform.Find("DetailPanel").gameObject;

        if (description == null)
            description = detailPanel.transform
                .Find("Description")
                .GetComponent<TextMeshProUGUI>();

        if (currentLevel == null)
            currentLevel = detailPanel.transform
                .Find("CurrentLevel")
                .GetComponent<TextMeshProUGUI>();

        if (nextLevel == null)
            nextLevel = detailPanel.transform
                .Find("NextLevel")
                .GetComponent<TextMeshProUGUI>();

        if (upgradeCostText == null)
            upgradeCostText = detailPanel.transform
                .Find("CostText")
                .GetComponent<TextMeshProUGUI>();

        if (upgradeButton == null)
            upgradeButton = detailPanel.transform
                .Find("UpgradeButton")
                .GetComponent<Button>();
    }

    void OnRuntimeChanged(SkillNodeRuntime node)
    {
        RefreshUI();
    }

    public void Show(SkillNodeRuntime node)
    {
        runtime = node;

        root.SetActive(true);

        runtime.OnLevelChanged += OnRuntimeChanged;

        RefreshUI();
    }

    void RefreshUI()
    {
        if (runtime == null) return;

        bool unlocked = runtime.IsUnlocked;

        header.SetActive(true);

        icon.sprite = runtime.Data.icon;
        skillName.text = runtime.Data.nodeName;

        if (!unlocked)
        {
            ShowUnlockPanel();
        }
        else
        {
            ShowDetailPanel();
        }
    }

    void ShowUnlockPanel()
    {
        unlockPanel.SetActive(true);
        detailPanel.SetActive(false);

        int cost = runtime.Data.GetUpgradeCost(runtime.Level);
        unlockCostText.text = "Cost: " + cost + " Skill Point";

        requirementText.text = BuildRequirementText();

        unlockButton.onClick.RemoveAllListeners();
        unlockButton.onClick.AddListener(OnUnlockClicked);
    }
    void ShowDetailPanel()
    {
        unlockPanel.SetActive(false);
        detailPanel.SetActive(true);

        description.text = runtime.Data.description;

        currentLevel.text =
            "Current Level: " + runtime.Level +
            "\n" + BuildStatText(runtime.Level);

        if (!runtime.IsMaxLevel)
        {
            int cost = runtime.Data.GetUpgradeCost(runtime.Level);

            nextLevel.text =
                "Next Level: " + runtime.NextLevel +
                "\n" + BuildStatText(runtime.NextLevel);

            upgradeCostText.text = "Upgrade Cost: " + cost + " Skill Point";
        }
        else
        {
            nextLevel.text = "Max Level";
            upgradeCostText.text = "";
        }

        upgradeButton.onClick.RemoveAllListeners();
        upgradeButton.onClick.AddListener(OnUpgradeClicked);
    }

    void OnUnlockClicked()
    {
        PlayerManager.playerCtrl.SkillManager.UnlockNode(runtime.Data);
        RefreshUI();
    }

    void OnUpgradeClicked()
    {
        PlayerManager.playerCtrl.SkillManager.UpgradeSkillNode(runtime.Data);
        RefreshUI();
    }

    public void Hide()
    {
        if (runtime != null)
            runtime.OnLevelChanged -= OnRuntimeChanged;

        runtime = null;

        unlockPanel.SetActive(false);
        detailPanel.SetActive(false);

        root.SetActive(false);
    }

    string BuildStatText(int level)
    {
        if (runtime.Data.modifiers == null) return "";

        string text = "";

        foreach (var mod in runtime.Data.modifiers)
        {
            if (mod.values == null || mod.values.Count == 0)
                continue;

            int index = Mathf.Clamp(level - 1, 0, mod.values.Count - 1);
            float value = mod.values[index];

            switch (mod.type)
            {
                case SkillModifierType.skillDamage:
                    text += "Damage +" + value + "\n";
                    break;

                case SkillModifierType.skillDamagePercent:
                    text += "Damage +" + (value * 100f).ToString("0") + "%\n";
                    break;
                case SkillModifierType.buffFlatDamage:
                    text += "Buff Damage +" + value + "\n";
                    break;
                case SkillModifierType.buffPercentDamage:
                    text += "Buff Damage +" + (value * 100f).ToString("0") + "%\n";
                    break;
                case SkillModifierType.Duration:
                    text += "Duration +" + value + "s\n";
                    break;
                case SkillModifierType.CritChance:
                    text += "Crit Chance +" + (value * 100f).ToString("0") + "%\n";
                    break;
                case SkillModifierType.CritDame:
                    text += "Crit Damage +" + (value * 100f).ToString("0") + "%\n";
                    break;
                case SkillModifierType.Cost:    
                    text += "Cost +" + value + "\n";
                    break;
                case SkillModifierType.CooldownReduction:
                    text += "Cooldown Reduction +" + value + "\n";
                    break;
                case SkillModifierType.StunDuration:
                    text += "Stun Duration +" + value + "s\n";
                    break;
                case SkillModifierType.HealFlat:
                    text += "Heal +" + value + "\n";
                    break;
                case SkillModifierType.HealPercent:
                    text += "Heal +" + (value * 100f).ToString("0") + "%\n";
                    break;
            }
        }
        if (runtime.Data.statModifiers != null)
        {
            foreach (var stat in runtime.Data.statModifiers)
            {
                if (stat.values == null || stat.values.Count == 0)
                    continue;

                int index = Mathf.Clamp(level - 1, 0, stat.values.Count - 1);
                float value = stat.values[index];

                if (stat.modifierType == ModifierType.Percent)
                {
                    text += stat.statType + " +" + (value * 100f).ToString("0") + "%\n";
                }
                else
                {
                    if (stat.statType == StatType.CritChance || stat.statType == StatType.CritDamage)
                    {
                        text += stat.statType + " +" + (value * 100f).ToString("0") + "%\n";
                    }
                    else
                    {
                        text += stat.statType + " +" + value + "\n";
                    }
                }
            }
        }

        return text;
    }
    string BuildRequirementText()
    {
        if (runtime.Data.requiredNodes == null || runtime.Data.requiredNodes.Count == 0)
            return "No requirements";

        string text = "Requirements:\n";

        foreach (var req in runtime.Data.requiredNodes)
        {
            var node = PlayerManager.playerCtrl.SkillManager
                .SkillTreeSystem.Query.GetNode(req.nodeId);

            if (node == null) continue;

            text += node.Data.nodeName +
                    " Lv." + req.requiredLevel;
            text += "\n";
        }

        return text;
    }
}