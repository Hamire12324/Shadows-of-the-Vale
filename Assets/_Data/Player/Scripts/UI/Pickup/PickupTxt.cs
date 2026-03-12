using UnityEngine;

public class PickupTxt : TxtAbstract
{
    public void SetText(string value)
    {
        textMeshPro.text = value;
    }
    public void SetColor(Color color)
    {
        textMeshPro.color = color;
    }
}
