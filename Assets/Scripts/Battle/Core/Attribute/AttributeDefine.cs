


public enum AttributeType
{
    None = 0,
    MaxHp,
    Attack,
    Def,
    Psy,
    San,
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