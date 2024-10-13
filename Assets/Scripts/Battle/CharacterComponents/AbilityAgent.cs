
using System.Collections.Generic;

public class AbilityAgent : CharacterComponent
{
    protected List<AbilityBase> mAbilities = new List<AbilityBase>();
    public List<AbilityBase> Abilities => mAbilities;

    public void AddAbility(int abiKind)
    {
        
    }

    public override void OnLateUpdate(float deltaTime)
    {
        foreach (var abi in mAbilities)
        {
            abi.Update(deltaTime);
        }
    }
}