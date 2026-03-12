using TMPro;
using UnityEngine;

public class DamageTextCtrl : PoolObj
{
    [SerializeField] protected TextMeshProUGUI txt;
    protected override void OnEnable()
    {
        base.OnEnable();

        txt.color = Color.white;
        transform.localScale = Vector3.one;
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadText();
    }
    protected virtual void LoadText()
    {
        if (txt != null) return;
        txt = GetComponentInChildren<TextMeshProUGUI>(true);
    }
    public void SetDamage(float damage, bool crit = false)
    {
        txt.text = damage.ToString();

        if (crit)
        {
            txt.color = Color.yellow;
            transform.localScale = Vector3.one * 1.5f;
        }
    }
}