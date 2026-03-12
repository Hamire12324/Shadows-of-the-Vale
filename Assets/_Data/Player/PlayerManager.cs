using UnityEngine;

public class PlayerManager : BaseMonoBehaviour
{
    public static PlayerCtrl playerCtrl;

    [SerializeField] private PlayerCtrl debugPlayerCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        debugPlayerCtrl = playerCtrl;
    }
}