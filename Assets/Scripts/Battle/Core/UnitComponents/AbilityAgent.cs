
using System.Collections.Generic;

// unit ability manager
public class AbilityAgent : UnitComponent
{
    protected List<AbilityBase> mAbilities = new List<AbilityBase>();
    public List<AbilityBase> Abilities => mAbilities;

    public override void Start()
    {
        foreach (var abi in mOwner.Data.mTalents)
        {
            RegisterAbility(abi);
        }
    }

    public AbilityBase RegisterAbility(int abiKind)
    {
        AbilityBase newAbi = AbilityFactory.CreateAbility(abiKind);

        if (newAbi != null)
        {
            newAbi.Unit = Owner;
            newAbi.Init();
            
            mAbilities.Add(newAbi);
        }
        
        return newAbi;
    }
    
    public void RemoveAbility(AbilityBase abiInstance)
    {
        if (abiInstance != null)
        {
            abiInstance.Dispose();
            mAbilities.Remove(abiInstance);
        }
    }

    public AbilityBase GetAbilityByType(AbilityType type)
    {
        foreach (var abi in mAbilities)
        {
            if (abi.Data.mType == type)
            {
                return abi;
            }
        }

        return null;
    }

    public override void LateUpdate(float deltaTime)
    {
        foreach (var abi in mAbilities)
        {
            abi.Update(deltaTime);
        }
    }

    public override void OnSelfDeath(DamageReport report)
    {
        foreach (var abi in Abilities)
        {
            abi.OnSelfDeath(report);
        }
    }
    
    
    #region DamageProcess

    /// <summary>
    /// 造成伤害计算预处理
    /// </summary>
    public void PreDealDamageCalc(DealDamageCalc damageCalc)
    {
        if ((damageCalc.mFlag & DealDamageFlag.Fixed) == 0)
        {
            foreach (var abi in Abilities)
            {
                if (abi == damageCalc.mAbility)
                {
                    abi.OnPreDealDamageCalc(damageCalc);
                }
                else
                {
                    abi.OnPreDealDamageCalcOtherAbility(damageCalc);
                }
            }
        }
    }

    public void OnPreTakeDamageCalc(TakeDamageCalc dmgCalc)
    {
        foreach (var abi in Abilities)
        {
            abi.OnPreTakeDamageCalc(dmgCalc);
        }
    }

    public void OnAllyPreTakeDamageCalc(TakeDamageCalc dmgCalc)
    {
        foreach (var abi in Abilities)
        {
            abi.OnAllyPreTakeDamageCalc(dmgCalc);
        }
    }

    public void PreDealDamage(DamageReport report)
    {
        foreach (var abi in Abilities)
        {
            if (report.mMeta.mAbility == abi)
            {
                abi.OnPreDealDamage(report);
            }
            else
            {
                abi.OnPreDealDamageOtherAbility(report);
            }
        }
    }

    public void PreTakeDamage(DamageReport report)
    {
        foreach (var abi in Abilities)
        {
            abi.OnPreTakeDamage(report);
        }
    }

    public void PostDealDamage(DamageReport report)
    {
        foreach (var abi in Abilities)
        {
            if (abi == report.mMeta.mAbility)
            {
                abi.OnPostDealDamage(report);
            }
            else
            {
                abi.OnPostDealDamageOtherAbility(report);
            }
        }
    }

    public void PostTakeDamage(DamageReport report)
    {
        foreach (var abi in Abilities)
        {
            abi.OnPostTakeDamage(report);
        }
    }

    public void AllyDealDamage(DamageReport report)
    {
        foreach (var abi in Abilities)
        {
            abi.OnAllyDealDamage(report);
        }
    }

    public void AllyTakeDamage(DamageReport report)
    {
        foreach (var abi in Abilities)
        {
            abi.OnAllyTakeDamage(report);
        }
    }

    public void EnemyDealDamage(DamageReport report)
    {
        foreach (var abi in Abilities)
        {
            abi.OnEnemyDealDamage(report);
        }
    }

    public void EnemyTakeDamage(DamageReport report)
    {
        foreach (var abi in Abilities)
        {
            abi.OnEnemyTakeDamage(report);
        }
    }

    #endregion
}