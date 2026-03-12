using TMPro;
using UnityEngine;

public class TxtAbstract : BaseMonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI textMeshPro;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTextMeshPro();
    }
    protected virtual void LoadTextMeshPro()
    {
        if (this.textMeshPro != null) return;
        this.textMeshPro = GetComponent<TextMeshProUGUI>();
        if (this.textMeshPro == null) Debug.LogError($"{name}: TextMeshPro not found");
    }
}
