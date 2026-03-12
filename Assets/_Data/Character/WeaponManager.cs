using UnityEngine;

public class WeaponManager : CharacterAbstract
{

    [SerializeField] private WeaponDamSender weaponDamSender;
    public WeaponDamSender WeaponDamSender => weaponDamSender;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCurrentWeapon();
    }
    protected virtual void LoadCurrentWeapon()
    {
        if (this.weaponDamSender != null) return;
        this.weaponDamSender = this.characterCtrl.CharacterWeaponHolder.
            GetComponentInChildren<WeaponDamSender>();
        Debug.Log(transform.name + ": LoadCurrentWeapon", gameObject);
    }
    public void EquipWeapon(GameObject weaponPrefab)
    {
        if (this.weaponDamSender != null)
        {
            Destroy(weaponDamSender.gameObject);
        }

        GameObject weaponObj = Instantiate(weaponPrefab, this.characterCtrl.CharacterWeaponHolder.transform);
        this.weaponDamSender = weaponObj.GetComponent<WeaponDamSender>();

        if (this.weaponDamSender != null)
        {
            weaponDamSender.Init(characterCtrl);
        }
    }

    public void EnableDamage()
    {
        this.weaponDamSender?.EnableDamage();
    }

    public void DisableDamage()
    {
        this.weaponDamSender?.DisableDamage();
    }
}