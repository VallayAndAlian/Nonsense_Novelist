
public class BattleObject
{
    protected int mID = 0;
    public int ID
    {
        set => mID = value;
        get => mID;
    }

    protected bool mRegistered = false;
    public bool IsRegistered
    {
        set => mRegistered = value;
        get => mRegistered;
    }

    protected bool mTickEnable = true;

    public bool IsTickEnable
    {
        set => mTickEnable = value;
        get => mTickEnable;
    }
    
    public virtual void OnRegistered() {}
    public virtual void Init() {}
    public virtual void Start() {}
    public virtual void Update(float deltaSec) {}
    public virtual void LateUpdate(float deltaSec) {}
}