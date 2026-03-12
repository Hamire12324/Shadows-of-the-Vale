using UnityEngine;
using UnityEngine.UI;
public class ImageUI : BaseMonoBehaviour
{
    [SerializeField] protected Image image;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadImage();
    }
    protected virtual void LoadImage()
    {
        if (this.image != null) return;
        this.image = GetComponent<Image>();
        Debug.Log(transform.name + ": LoadImage", gameObject);
    }
}
