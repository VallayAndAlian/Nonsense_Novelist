using UnityEngine;
using UnityEngine.UI;
public class BattleUnitSelfUI : BattleUI
{
    protected bool mEnabled = false;
    protected bool mRegistered = false;

    public class UIOwner
    {
        public BattleUnit mUnit = null;
        public Transform mUnitPos;

    }

    protected UIOwner mOwner;
    public UIOwner Owner
    {
        set
        {
            mOwner = value;
            mRegistered = true;
            OnRegistered();
        }

        get => mOwner;
    }


    protected Transform mObject;
    public Transform Object => mObject;

    public class UnitUIContent
    {
        public Slider hpSlider = null;
        public Transform mUnitPos;

    }

    protected UnitUIContent mUIContent;
    public UnitUIContent UIContent => mUIContent;



    public Vector3 offset = new Vector3(0, 2, 0);


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

    public override void Start() 
    {
        AddListenerToAttribute();
    }

    public override void Update(float deltaTime) 
    {
        FollowPlayerPos();
    }

    public override void LateUpdate(float deltaTime) { }



    protected void FollowPlayerPos()
    {
        if (Owner == null) return;

        Vector3 worldPosition = Owner.mUnitPos.position + offset;
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            Object.parent as RectTransform,
            screenPosition,
            Camera.main,
            out Vector2 localPosition
        );

        Object.localPosition = localPosition;
    }

    public void AddListenerToAttribute()
    {
        if (Owner == null) return;

        Owner.mUnit.AttributeSet.OnAttributeChanged += RefreshPanel;
    }

    protected void RefreshPanel(AttributeSet atr)
    {
        if (Owner == null) return;

        if (UIContent.hpSlider != null)
            UIContent.hpSlider.value = atr.GetAttribute(AttributeType.MaxHp).mValue 
                / atr.GetAttribute(AttributeType.MaxHp).mValue;
    }
}
