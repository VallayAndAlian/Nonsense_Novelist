

using System.Collections.Generic;
using UnityEngine;

public class AbilityBase
{
    protected BattleUnit mUnit = null;

    public BattleUnit Unit
    {
        set => mUnit = value;
        get => mUnit;
    }

    protected AbilityTable.Data mData = null;
    public AbilityTable.Data Data
    {
        set => mData = value;
        get => mData;
    }
    
    public void Init()
    {
        OnInit();
    }

    protected virtual void OnInit() { }

    public void Update(float deltaTime)
    {
        if (Unit.IsAlive)
        {
            Tick(deltaTime);
        }

        TickEvenWhenDead(deltaTime);
    }
    
    protected virtual void Tick(float deltaTime) { }

    protected virtual void TickEvenWhenDead(float deltaTime) { }

    #region DamageProcess
    
    public virtual void OnPreDealDamageCalc(DealDamageCalc dmgCalc) { }

    public virtual void OnPreDealDamageCalcOtherAbility(DealDamageCalc dmgCalc) { }

    public virtual void OnPreTakeDamageCalc(TakeDamageCalc dmgCalc) { }

    public virtual void OnAllyPreTakeDamageCalc(TakeDamageCalc dmgCalc) { }
    

    public virtual void OnPreDealDamage(DamageReport report) { }
    
    public virtual void OnPreDealDamageOtherAbility(DamageReport report) { }

    public virtual void OnPostDealDamage(DamageReport report) { }

    public virtual void OnPostDealDamageOtherAbility(DamageReport report) { }
    
    public virtual void OnAllyDealDamage(DamageReport report) { }

    public virtual void OnEnemyDealDamage(DamageReport report) { }
    

    public virtual void OnPreTakeDamage(DamageReport report) { }

    public virtual void OnAllyPreTakeDamage(DamageReport report) { }

    public virtual void OnPostTakeDamage(DamageReport report) { }
    
    public virtual void OnAllyTakeDamage(DamageReport report) { }

    public virtual void OnEnemyTakeDamage(DamageReport report) { }
    
    public virtual void OnPawnDeath(AbstractCharacter deceased, DamageReport report) { }
    
    public virtual void OnSelfDeath(DamageReport report) { }
    
    #endregion

    #region EffectProcess

    

    #endregion
    
    #region ParseCustomParams

    public class Formula
    {
        public string mKey;
        public List<float> mValues;
        
        public Formula(string key)
        {
            mKey = key;
        }

        public float Evaluate(AbilityBase abi)
        {
            return mValues[0];
        }
        
        public bool EvaluateBool(AbilityBase abi)
        {
            return mValues[0] > 0.5;
        }
        
        public int EvaluateInt(AbilityBase abi)
        {
            return (int)mValues[0];
        }
    }

    protected List<Formula> mParams;

    public virtual void AddParams() {}
    
    public bool ParseParams()
    {
        AddParams();
        
        foreach (var param in mParams)
        {
            if (!ReadParam(param))
            {
                return false; 
            }
        }
        
        return true; 
    }
    
    protected bool ReadParam(Formula param)
    {
        if (Data.mCustomParams.TryGetValue(param.mKey, out var data))
        {
            param.mValues = data.mValues;
            return true;
        }
        
        return false;
    }

    #endregion
}