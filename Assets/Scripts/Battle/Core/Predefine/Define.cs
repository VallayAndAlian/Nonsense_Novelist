
using System;

public enum BattleState
{
    None = 0,
    Inprogress,
    End,
}

public enum BattleMode
{
    None = 0,
    Normal,
    TestShoot,
}

public enum BattleUnitCareerAnchor
{
    None= 0,
    Soldier,//战士
    Monk,//法师
    Tank,//坦克
    Shooter,//射手
    Summon//召唤
}
public enum BattleUnitCareerType
{
    None = 0,
    InitCareer,
    Swordsman,//剑客
    Wizard,//巫师
    Jester,//弄臣
    Boxer,//拳击手
    Gunner,//炮手
    Follower,//信徒
}

[Flags]
public enum BattleCamp : int
{
    None = 0,
    Neutral = 1 << 0,
    Camp1 = 1 << 1,
    Camp2 = 1 << 2,
    Camp3 = 1 << 3,
    Camp4 = 1 << 4,
    Camp5 = 1 << 5,
    Camp6 = 1 << 6,
    Camp7 = 1 << 7,
    Camp8 = 1 << 8,
    Boss = 1 << 7,
}

[Flags]
public enum BattleRelation : int
{
    None = 0,
    Self = 1 << 0,
    Ally = 1 << 1,
    Enemy = 1 << 2,
}

public enum DamageSource
{
    None = 0,
    AutoAttack,
    Ability,
}

public enum DamageType
{
    None = 0,
    Fix,
    Psy,
    Magic,
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