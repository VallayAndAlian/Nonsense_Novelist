
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class AbilityTrigger : AbilityModule
{
    public enum Type
    {
        Undefined = 0,
        Direct = 1,                                // 直接触发
        Tick = 2,                                  // 每隔多少秒
        DirectCall = 3,                            // 外部驱动
        
        /* 伤害类 */
        AutoAttackTimes = 30,                       // 每隔几次自动攻击
        BeAutoAttackTimes = 31,                     // 每受到几次自动攻击
        TakeDamageByValue = 32,                     // 每受到几点伤害
        
        /* 死亡类 */
        AnyDeath = 100,                              // 任意单位死亡时
        SelfDeath = 101,                             // 自己死亡时
        AllyDeath = 102,                             // 友军死亡时
        EnemyDeath = 103,                            // 敌人死亡时
        ServantDeath = 104,                          // 随从死亡时

        SelfAttributeThreshold=130,                  //自身属性达到多少
        NounTreshold=131,                            //拥有多少名词时
    }

    public static AbilityTrigger Create(Type type)
    {
        AbilityTrigger trigger = null;

        switch (type)
        {
            case Type.Direct:
                trigger = new AMTDirect();
                break;
            
            case Type.Tick:
                trigger = new AMTTick();
                break;
            
            case Type.DirectCall:
                trigger = new AMTDirectCall();
                break;
            
            case Type.AutoAttackTimes:
                trigger = new AMTAttackTimes();
                break;
            
            case Type.AnyDeath:
                trigger = new AMTAnyDeath();
                break;
            
            case Type.SelfDeath:
                trigger = new AMTSelfDeath();
                break;
            
            case Type.AllyDeath:
                trigger = new AMTAllyDeath();
                break;
            
            case Type.EnemyDeath:
                trigger = new AMTEnemyDeath();
                break;
            
            case Type.ServantDeath:
                trigger = new AMTServantDeath();
                break;

            case Type.SelfAttributeThreshold:
                trigger = new AMTSelfAttributeThreshold();
                break;
            case Type.NounTreshold:
                trigger = new NounTreshold();
                break;

            default:
                break;
        }

        if (trigger != null)
            trigger.mType = type;
        
        return trigger;
    }
    
    
    public Type mType = Type.Undefined;

    public AbilityTriggerTable.Data mData = null;
    
    protected int mMaxTriggerTimes = int.MaxValue;
    protected float mPossibility = 1.0f;
    protected float mCoolDownDuration = 0f;
    
    
    protected int mTriggerTimes = 0;
    protected float mLastTriggerTime = 0f;

    public bool CoolDown
    {
        get
        {
            if (mTriggerTimes <= 0 || mCoolDownDuration < 0.01f)
                return true;
            else
                return mOwner.ElapsedSec - mLastTriggerTime >= mCoolDownDuration;
        }
    }
    
    public bool TriggerTimesReachLimit => mMaxTriggerTimes > 0 && mTriggerTimes >= mMaxTriggerTimes;

    public virtual bool Setup(AbilityTriggerTable.Data data)
    {
        mData = data;
        
        mMaxTriggerTimes = mData.mMaxTriggerTimes;
        mPossibility = mData.mPossibility;
        mCoolDownDuration = mData.mCoolDownDuration;
        
        mCustomParams = data.mCustomParams;
        
        return ParseParams();
    }

    protected bool TryTrigger(object triggerData)
    {
        if (ShouldTrigger())
        {
            mTriggerTimes++;
            mLastTriggerTime = mOwner.ElapsedSec;
            
            mOwner.TriggeredBy(triggerData);
            return true;
        }

        return false;
    }

    public virtual bool ShouldTrigger()
    {
        if (!CoolDown)
            return false;

        if (TriggerTimesReachLimit)
            return false;
        
        if (mPossibility < 1 && Random.Range(0f, 1.0f) >= mPossibility)
            return false;
        
        return true;
    }

    public virtual bool CanTriggerByOther() { return false; }

    public virtual void TriggerDirect() {}

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
    
    public virtual void OnPawnDeath(BattleUnit deceased, DamageReport report) { }
    
    public virtual void OnSelfDeath(DamageReport report) { }
}

public class AMTDirect : AbilityTrigger
{
    public override void OnInit()
    {
        TryTrigger(null);
    }
}

public class AMTTick : AbilityTrigger
{
    public override void Update(float deltaTime)
    {
        TryTrigger(null);
    }
}


public class AMTDirectCall : AbilityTrigger
{
    public override bool CanTriggerByOther()
    {
        return true;
    }

    public override void TriggerDirect()
    {
        TryTrigger(null);
    }
}


public class AMTAttackTimes : AbilityTrigger
{
    protected Formula mAttackTimes = new Formula("attack_times");
    protected int mCurrentAttackTimes = 0;
    
    public override void AddParams()
    {
        mParams.Add(mAttackTimes);
    }

    public override void OnPreDealDamage(DamageReport report)
    {
        if (report.mMeta.mAbility != null && report.mMeta.mAbility.Data.mType == AbilityType.AutoAttack)
        {
            ++mCurrentAttackTimes;
            if (mCurrentAttackTimes >= mAttackTimes.EvaluateInt(mOwner))
            {
                mCurrentAttackTimes = 0;
                TryTrigger(report);
            }
        }
    }
}

public class AMTAnyDeath : AbilityTrigger
{
    public override void OnPawnDeath(BattleUnit deceased, DamageReport report)
    {
        TryTrigger(report);
    }
}

public class AMTSelfDeath : AbilityTrigger
{
    public override void OnSelfDeath(DamageReport report)
    {
        TryTrigger(report);
    }
}

public class AMTAllyDeath : AbilityTrigger
{
    public override void OnPawnDeath(BattleUnit deceased, DamageReport report)
    {
        if (deceased.Camp == mOwner.Camp)
        {
            TryTrigger(report);
        }
    }
}

public class AMTEnemyDeath : AbilityTrigger
{
    public override void OnPawnDeath(BattleUnit deceased, DamageReport report)
    {
        if (deceased.Camp != mOwner.Camp)
        {
            TryTrigger(report);
        }
    }
}

public class AMTServantDeath : AbilityTrigger
{
    public override void OnPawnDeath(BattleUnit deceased, DamageReport report)
    {
        if (deceased.ServantOwner == mOwner.Unit)
        {
            TryTrigger(report);
        }
    }
}
public class AMTSelfAttributeThreshold : AbilityTrigger
{
    protected Formula mTargetAttribute = new Formula("attribute_type");
    protected Formula mComparisonOp = new Formula("comparison_op");
    protected Formula mThreshold = new Formula("threshold");
    public override void AddParams()
    {
       mParams.AddRange(new[] { mTargetAttribute, mComparisonOp, mThreshold });
    }
    public override void Update(float deltaTime)
    {
        if (CheckCondition())
        {
            TryTrigger(null);
        }
    }
    private bool CheckCondition()
    {
        var attributeType = (AttributeType)mTargetAttribute.EvaluateInt(mOwner);
        var currentValue = mOwner.Unit.GetAttributeValue(attributeType);
        var threshold = mThreshold.Evaluate(mOwner)*mOwner.Unit.MaxHp;

        return CompareValues(
            currentValue,
            threshold,
            (ComparisonOperator)mComparisonOp.EvaluateInt(mOwner)
        );
    }
    private bool CompareValues(float a, float b, ComparisonOperator op)
    {
        return op switch
        {
            ComparisonOperator.LessThan => a < b,
            ComparisonOperator.LessOrEqual => a <= b,
            ComparisonOperator.Equal => Mathf.Approximately(a, b),
            ComparisonOperator.GreaterOrEqual => a >= b,
            ComparisonOperator.GreaterThan => a > b,
            _ => false
        };
    }
}
public class NounTreshold : AbilityTrigger
{
    protected Formula mNounTreshold = new Formula("noun_treshold");
    protected int mCurrentNounNum;
    public override void OnInit()
    {
        mCurrentNounNum=mOwner.Unit.WordComponent.GetWordsByType(WordType.Noun).Count;
    }
    public override void AddParams()
    {
        mParams.Add(mNounTreshold);
    }
    public override void Update(float deltaTime)
    {
        mCurrentNounNum = mOwner.Unit.WordComponent.GetWordsByType(WordType.Noun).Count;
        if (mCurrentNounNum>=mNounTreshold.EvaluateInt(mOwner))
        {
            TryTrigger(null);
        }
    }
}
