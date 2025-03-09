﻿

public class UnitComponent
{
    protected BattleUnit mOwner = null;
    protected bool mEnabled = true;
    protected bool mRegistered = false;

    public BattleUnit Owner
    {
        set
        {
            mOwner = value;
            mRegistered = true;
            OnRegistered();
        }

        get => mOwner;
    }

    public bool Enabled
    {
        set
        {
            if (mEnabled == value) 
                return;
            
            mEnabled = value;
            if (mEnabled)
            {
                OnEnabled();
            }
            else
            {
                OnDisabled();
            }
        }

        get => mEnabled;
    }

    public bool IsRegistered => mRegistered;

    protected virtual void OnRegistered() { }

    protected virtual void OnEnabled() { }
    
    protected virtual void OnDisabled() { }
    

    public virtual void Start() { }
    
    public virtual void Update(float deltaTime) { }
    
    public virtual void LateUpdate(float deltaTime) { }
    
}