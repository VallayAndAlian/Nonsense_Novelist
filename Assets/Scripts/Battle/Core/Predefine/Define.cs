
using System;

public enum BattleState
{
    None = 0,
    Inprogress,
    End,
}

public enum DamageSource
{
    None = 0,
    AutoAttack,
    Ability,
}

[Flags]
public enum DealDamageFlag
{
    None = 0,
    Fixed = 1,
    AlwaysHit = 2,
    AlwaysCritical = 4,
}

[Flags]
public enum TakeDamageFlag
{
    Dodged = 1,
    Critical = 2,
    Blocked = 4,
    Immune = 8,
}