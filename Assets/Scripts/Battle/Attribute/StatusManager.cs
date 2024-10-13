using System;

[Flags]
public enum Status
{
    None = 0,
    Stun = 0x1,
}

public class StatusManager
{
    protected Status mStatus = Status.None;
    public Status Status => mStatus;
    
    protected Status mStatusMod = Status.None;
    
    public bool InStatus(Status status)
    {
        return (mStatus & status) == status;
    }

    public void AddStatus(Status status)
    {
        mStatusMod |= status;
    }

    public void Apply()
    {
        mStatus = mStatusMod;
        mStatusMod = 0;
    }
}