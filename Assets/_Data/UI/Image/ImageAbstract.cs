using UnityEngine;
using UnityEngine.UI;

public class ImageAbstract : BaseMonoBehaviour
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
        if (this.image == null) Debug.LogError($"{name}: Image not found");
    }
}
