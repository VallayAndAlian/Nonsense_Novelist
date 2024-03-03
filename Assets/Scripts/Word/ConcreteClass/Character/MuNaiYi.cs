using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 木乃伊
/// </summary>
class MuNaiYi : AbstractCharacter
{
    override public void Awake()
    {
        base.Awake();

        //基础信息
        characterID = 4;
        wordName = "木乃伊";
        bookName = BookNameEnum.EgyptMyth;
        brief = "暂无文案";
        description = "沉默者曾经是某个王国的贵族，从小便精通魔法的使用。" +
            "据说在王国政权被颠覆之后他被叛逃者关在地牢里折磨，痛苦的嘶吼随着时日慢慢沉寂，" +
            "直到他所有人性都被磨灭。而他身上的魔法能量也被扭曲，变得恶毒不堪。如今的它都蜷缩于阴暗的地窖里，练习着被巫师们唾弃的魔法。";

        //数值
        hp = maxHp = 100;
        atk = 3;
        def = 5;
        psy = 3;
        san = 3;

        attackInterval = 2.2f;
        AttackTimes = 1;
        attackSpeedPlus = 1;
        attackDistance = 500;
        myState.aimCount = 1;
        attackAmount = 1;
        hasBetray = false;

        //特性
        roleName = "再生";
        roleInfo = "每次复活攻击+3防御+6，出场获得“复活”";


    }
    NoHealthTrigger _nht = null;
    private void Start()
    {  // 每次复活攻击 + 3防御 + 6”
        var _list = myState.allState.Find(p => p.id == AI.StateID.attack).triggers;
        foreach (var _t in _list)
        {
            if (_t.GetType() == typeof(AI.NoHealthTrigger))
            { (_t as AI.NoHealthTrigger).event_relife += OnLive; _nht = (_t as AI.NoHealthTrigger); }

        }

        //出场获得“复活”
        var re =gameObject.AddComponent<ReLife>();
        re.maxTime = Mathf.Infinity;//复活技能
  
    }

    /// <summary>
    /// 身份
    /// </summary>
    public void OnLive()
    {
        atk += 3;
        def += 6;

    }

    private void OnDestroy()
    {
        if (_nht != null)
        {
            _nht.event_relife -= OnLive;
        }
    }
    public override string ShowText(AbstractCharacter otherChara)
    {
        if (otherChara != null)
            return otherChara.wordName + "早已看见多了一个妹妹，细看形容，只见泪光点点，娇喘微微，闲静时如姣花照水，行动处似弱柳扶风，" + otherChara.wordName + "笑道：“这个妹妹，我曾见过的”";
        else
            return null;
    }
    public override string CriticalText(AbstractCharacter otherChara)
    {
        if (otherChara != null)
            return "“我就知道，别人不挑剩下的也不给我。”林黛玉轻捻一朵花瓣，向" + otherChara.wordName + "飞去";
        else
            return null;
    }

    public override string LowHPText()
    {
        return "黛玉对侍女喘息道：“笼上火盆罢。”便将一对帕子，一叠诗稿焚尽于火盆中。";
    }
    public override string DieText()
    {
        return "“宝玉…宝玉…你好……”黛玉没说完便合上了双眼。";
    }

}
