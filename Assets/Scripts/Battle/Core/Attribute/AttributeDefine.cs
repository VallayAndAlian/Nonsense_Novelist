


public enum AttributeType
{
    None = 0,
    MaxHp,
    Attack,
    AttackSpeed,
    Def,
    Psy,
    San,
}
public enum ComparisonOperator
{
    LessThan,
    LessOrEqual,
    Equal,
    GreaterOrEqual,
    GreaterThan
}

public class Attribute
{
    public AttributeType mType;
    public float mOriginValue;
    public float mBaseValue;
    public float mValue;
    public float mMod;
    public float mPercentMod;
}