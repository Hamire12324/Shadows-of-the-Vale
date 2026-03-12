using System.Collections.Generic;

public class ResourceManager
{
    private Dictionary<ResourceType, ResourceSystem> resources = new();

    private CharacterCtrl character;

    public ResourceManager(CharacterCtrl character)
    {
        this.character = character;
        InitResources();
    }

    public void InitResources()
    {
        if (resources.ContainsKey(ResourceType.Mana)) return;

        resources[ResourceType.Mana] =
            new ResourceSystem(character.PlayerStat.MaxMP.FinalValue);
    }

    public ResourceSystem GetResource(ResourceType type)
    {
        resources.TryGetValue(type, out var res);
        return res;
    }
}