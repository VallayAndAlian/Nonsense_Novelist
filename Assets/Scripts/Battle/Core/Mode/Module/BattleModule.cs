
public class BattleModule
{
    private BattleBase _mBattle;
    public BattleBase Battle
    {
        get => _mBattle;
        set => _mBattle = value;
    }

    public virtual void Init() {}
    
    public virtual void Begin() {}
    
    public virtual void PostBegin() {}
    
    public virtual void Update(float deltaSec) {}
    
    public virtual void LateUpdate(float deltaSec) {}
    
    public virtual void Dispose() {}
}