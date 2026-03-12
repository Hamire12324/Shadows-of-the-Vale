using UnityEngine;

public class PlayerComboSystem : CharacterCombo
{
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadProfiles();
    }

    private void LoadProfiles()
    {
        if (attackProfiles != null && attackProfiles.Length > 0) return;

        attackProfiles = Resources.LoadAll<AttackProfile>("AttackProfiles");

        Debug.Log($"{transform.name} : Loaded {attackProfiles.Length} profiles", gameObject);
    }
}