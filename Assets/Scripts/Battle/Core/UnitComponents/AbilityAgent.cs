
using System.Collections.Generic;

public class AbilityAgent : UnitComponent
{
    protected List<AbilityBase> mAbilities = new List<AbilityBase>();
    public List<AbilityBase> Abilities => mAbilities;

    public AbilityBase RegisterAbility(int abiKind)
    {
        AbilityBase newAbi = AbilityFactory.CreateAbility(abiKind);

        if (newAbi != null)
        {
            newAbi.Character = Owner;
            newAbi.Init();
            
            mAbilities.Add(newAbi);
        }
        
        return newAbi;
    }
    
    public void RemoveAbility(AbilityBase abiInstance)
    {
        mAbilities.Remove(abiInstance);
    }

    public override void LateUpdate(float deltaTime)
    {
        foreach (var abi in mAbilities)
        {
            abi.Update(deltaTime);
        }
    }
}