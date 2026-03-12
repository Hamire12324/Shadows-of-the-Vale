using UnityEngine;

public class AnimEvent_EnemyRelay : AnimEvent_BaseRelay
{
    public override void EnableWeaponDamage()
    {
        var weaponSender = characterCtrl?.WeaponManager?.WeaponDamSender;
        if (weaponSender == null) return;

        weaponSender.SetDamageInfo(characterCtrl.CharacterCombo.BuildDamageInfo());
        weaponSender.EnableDamage();
    }
}
