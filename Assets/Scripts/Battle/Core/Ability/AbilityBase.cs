

public class AbilityBase
{
    protected BattleUnit mCharacter = null;

    public BattleUnit Character
    {
        set => mCharacter = value;
        get => mCharacter;
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

    public virtual bool ParseParams() { return false; }

    public void Update(float deltaTime)
    {
        if (Character.IsAlive)
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
}