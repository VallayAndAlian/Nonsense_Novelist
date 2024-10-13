

public class CharacterComponent
{
    protected AbstractCharacter mOwner = null;
    protected bool mEnabled = false;
    protected bool mRegistered = false;

    public AbstractCharacter Owner
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
    

    public virtual void OnStart() { }
    
    public virtual void OnUpdate(float deltaTime) { }
    
    public virtual void OnLateUpdate(float deltaTime) { }
    
}