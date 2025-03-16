using UnityEngine;
using UnityEngine.UI;
public class BattleUnitSelfUI : BattleUI
{
    protected bool mEnabled = false;
    protected bool mRegistered = false;
    public BattleUnitSelfUI(BattleUnit _role)
    { 
        mOwner=new UIOwner();
        mOwner.mUnit=_role;
        mOwner.mUnit.infoUI=this;
        mOwner.mUnitPos=mOwner.mUnit.UnitView.transform;
        AddListenerToAttribute();

        mUIContent=new UnitUIContent();
       
    } 


   protected override void CreateUIpanel()
    {
        mUIPanel=ResMgr.GetInstance().Load<GameObject>("UI/Battle/battleUnitSelfUI");
        mUIContent.hpSlider=mUIPanel.transform.Find("HP").GetComponent<Slider>();
    }


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


   

    public class UnitUIContent
    {
        public Slider hpSlider = null;

    }

    protected UnitUIContent mUIContent;
    public UnitUIContent UIContent => mUIContent;



    public Vector3 offset = new Vector3(0, 1.2f, 0);


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

    protected virtual void OnDisabled() {

     }

    public override void Init() 
    {
       
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
            UIPanel.transform.parent as RectTransform,
            screenPosition,
            null,
            out Vector2 localPosition
        );
        UIPanel.transform.localScale = Vector3.one*0.1f;
         UIPanel.transform.localPosition = localPosition;
         
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
            UIContent.hpSlider.value = Owner.mUnit.Hp 
                / atr.GetAttribute(AttributeType.MaxHp).mValue;
    }
}
