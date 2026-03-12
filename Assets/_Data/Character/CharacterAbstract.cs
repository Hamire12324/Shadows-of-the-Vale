using UnityEngine;

public class CharacterAbstract : BaseMonoBehaviour
{
    [SerializeField] protected CharacterCtrl characterCtrl;
    public CharacterCtrl CharacterCtrl => characterCtrl;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCharacterCtrl();
    }
    protected virtual void LoadCharacterCtrl()
    {
        if (characterCtrl != null) return;
        characterCtrl = GetComponentInParent<CharacterCtrl>(true);
        Debug.Log(transform.name + ": LoadCharacterCtrl", gameObject);
    }
}
