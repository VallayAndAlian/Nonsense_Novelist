

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleUnit : BattleObject
{
    protected bool mAlive = false;
    public bool IsAlive => mAlive;
    
    protected bool mStart = false;
    public bool IsStart => mStart;
    
    protected float mHp = 0;
    public float Hp => mHp;
    
    // camp would be enum
    protected int mCamp = 0;
    public int Camp
    {
        get => mCamp;
        set => mCamp = value;
    }

    protected BattleUnitTable.Data mData = null;
    
    
    protected List<UnitComponent> mComponents = new List<UnitComponent>();
    public List<UnitComponent> Components => mComponents;
    
    
    protected AbilityAgent mAbilityAgent = null;
    public AbilityAgent AbilityAgent => mAbilityAgent;
    
    protected EffectAgent mEffectAgent = null;
    public EffectAgent EffectAgent => mEffectAgent;
    

    protected StatusManager mStatus = new StatusManager();
    public StatusManager Status => mStatus;
    
    protected AttributeSet mAttributeSet = new AttributeSet();
    public AttributeSet AttributeSet => mAttributeSet;
    

    protected List<BattleUnit> mAllies = new List<BattleUnit>();
    public List<BattleUnit> Allies => mAllies;
    
    protected List<BattleUnit> mEnemies = new List<BattleUnit>();
    public List<BattleUnit> Enemies => mEnemies;

    protected UnitViewBase mView = null;

    public UnitViewBase UnitView
    {
        set => mView = value;
        get => mView;
    }
    

    public BattleUnit(BattleUnitTable.Data data)
    {
        mData = data;
    }

    public override void Init()
    {
        InitAttributes();
        AddComponents();
    }

    protected void InitAttributes()
    {
        AttributeSet.Define(AttributeType.MaxHp, mData.mMaxHp);
        AttributeSet.Define(AttributeType.Attack, mData.mAttack);
        AttributeSet.Define(AttributeType.Def, mData.mDefense);
        AttributeSet.Define(AttributeType.Psy, mData.mPsy);
        AttributeSet.Define(AttributeType.San, mData.mSan);
    }

    protected void AddComponents()
    {
        mAbilityAgent = new AbilityAgent();
        RegisterComponent(mAbilityAgent);
        
        mEffectAgent = new EffectAgent();
        RegisterComponent(mEffectAgent);
    }

    protected void RegisterComponent(UnitComponent component)
    {
        if (component.IsRegistered)
            return;
        
        component.Owner = this;
        mComponents.Add(component);

        if (IsStart)
            component.Start();
    }

    public float GetAttributeValue(AttributeType type)
    {
        return AttributeSet.Get(type);
    }

    public override void Start()
    {
        mHp = GetAttributeValue(AttributeType.MaxHp);

        foreach (var comp in Components)
        {
            comp.Start();
        }

        mStart = true;
    }

    public override void Update(float deltaSec)
    {
        foreach (var comp in Components.Where(comp => comp.Enabled))
        {
            comp.Update(deltaSec);
        }
    }

    public override void LateUpdate(float deltaSec)
    {
        foreach (var comp in Components.Where(comp => comp.Enabled))
        {
            comp.LateUpdate(deltaSec);
        }
        
        mAttributeSet.ApplyMod();
        mStatus.ApplyMod();
    }
    
    
    #region DamageProcess

    /// <summary>
    /// 造成伤害计算预处理
    /// </summary>
    public void PreDealDamageCalc(DealDamageCalc damageCalc)
    {
        if ((damageCalc.mFlag & DealDamageFlag.Fixed) == 0)
        {
            foreach (var abi in AbilityAgent.Abilities)
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
    
    /// <summary>
    /// 受到伤害计算预处理
    /// </summary>
    public TakeDamageCalc PreTakingDamageCalc(BattleUnit instigator, DealDamageCalc damageCalc)
    {
        TakeDamageCalc takeDamageCalc = BattleHelper.ReusableTakeDamageCalc;
        
        //todo: fill take damage calc info
        takeDamageCalc.mInstigator = instigator;
        takeDamageCalc.mAbility = damageCalc.mAbility;
        takeDamageCalc.mFlag = damageCalc.mFlag;
        takeDamageCalc.mMagic = damageCalc.mMagic;
        takeDamageCalc.mDefense = 0;
        takeDamageCalc.mResistance = 0;
        
        
        foreach (var abi in AbilityAgent.Abilities)
        {
            abi.OnPreTakeDamageCalc(takeDamageCalc);
        }

        // process ally
        foreach (var p in Allies)
        {
            foreach (var abi in p.AbilityAgent.Abilities)
            {
                abi.OnAllyPreTakeDamageCalc(takeDamageCalc);
            }
        }

        return takeDamageCalc;
    }

    public void PreDealDamage(DamageReport report)
    {
        foreach (var abi in AbilityAgent.Abilities)
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
        foreach (var abi in AbilityAgent.Abilities)
        {
            abi.OnPreTakeDamage(report);
        }
    }
    
    public void PostDealDamage(DamageReport report)
    {
        foreach (var abi in AbilityAgent.Abilities)
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
        foreach (var abi in AbilityAgent.Abilities)
        {
            abi.OnPostTakeDamage(report);
        }
    }

    public void AllyDealDamage(DamageReport report)
    {
        foreach (var abi in AbilityAgent.Abilities)
        {
            abi.OnAllyDealDamage(report);
        }
    }
    
    public void AllyTakeDamage(DamageReport report)
    {
        foreach (var abi in AbilityAgent.Abilities)
        {
            abi.OnAllyTakeDamage(report);
        }
    }
    
    public void EnemyDealDamage(DamageReport report)
    {
        foreach (var abi in AbilityAgent.Abilities)
        {
            abi.OnEnemyDealDamage(report);
        }
    }
    
    public void EnemyTakeDamage(DamageReport report)
    {
        foreach (var abi in AbilityAgent.Abilities)
        {
            abi.OnEnemyTakeDamage(report);
        }
    }

    public float ApplyDamage(float damageValue, bool cannotKill, out bool kill)
    {
        float number = 0;
        
        kill = false;

        if (damageValue > 0)
        {
            float currentMaxHp = GetAttributeValue(AttributeType.MaxHp);
            float max = cannotKill ? currentMaxHp - 1 : currentMaxHp;
            number = Mathf.Max(damageValue, max);
            
            mHp -= number;
            if (mHp < 0.99f)
            {
                kill = true;
            }
        }

        return number;
    }

    public void Die(DamageReport report)
    {
        if (!mAlive)
            return;

        mHp = 0f;
        mAlive = false;

        foreach (var abi in AbilityAgent.Abilities)
        {
            abi.OnSelfDeath(report);
        }

        // todo: 死亡事件通知
    }

    #endregion
}