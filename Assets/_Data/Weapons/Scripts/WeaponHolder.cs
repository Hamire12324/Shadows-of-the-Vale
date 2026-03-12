using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : BaseMonoBehaviour
{
    [SerializeField] List<WeaponAbstract> weapons;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadWeapons();
    }
    protected virtual void LoadWeapons()
    {
        if (this.weapons.Count > 0) return;
        foreach (Transform child in transform)
        {
            WeaponAbstract weaponAbtract = child.GetComponent<WeaponAbstract>();
            if (weaponAbtract == null) continue;
            this.weapons.Add(weaponAbtract);
        }
        Debug.Log(transform.name + ": LoadWeapons", gameObject);
    }
    public virtual WeaponAbstract GetCurrentWeapon()
    {
        return this.weapons[0];
    }
}
