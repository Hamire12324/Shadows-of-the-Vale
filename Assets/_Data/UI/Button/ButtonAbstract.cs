using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonAbstract : BaseMonoBehaviour
{
    [SerializeField] protected Button button;
    protected override void Start()
    {
        base.Start();
        this.AddOnClickEvent();
    }
    protected abstract void OnClick();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadButton();
    }
    protected virtual void LoadButton()
    {
        if (button != null) return;
        this.button = transform.GetComponent<Button>();
        Debug.Log(transform.name + ": LoadButton", gameObject);
    }
    protected virtual void AddOnClickEvent()
    {
        this.button.onClick.AddListener(this.OnClick);
    }
}
