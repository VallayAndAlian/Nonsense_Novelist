using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色：林黛玉
/// </summary>
class LinDaiYu : AbstractCharacter
{
    override public void Awake()
    {
       
        base.Awake();

        //基础信息
        characterID = 2;
        wordName = "林黛玉";
        bookName = BookNameEnum.HongLouMeng;
        brief = "《红楼梦》中一位性格敏感脆弱，却又极有灵性的少女。";
        description = "林黛玉，中国古典名著《红楼梦》的女主角，金陵十二钗正册双首之一，西方灵河岸绛珠仙草转世，最后于贾宝玉、薛宝钗大婚之夜泪尽而逝。她生得容貌清丽，兼有诗才，是古代文学作品中极富灵气的经典女性形象。" +
            "\n道是：" +
            "\n可叹停机德，堪怜咏絮才。" +
            "\n玉带林中挂，金簪雪里埋。";

        //数值
        hp = maxHp = 70;
        atk = 3;
        def = 3;
        psy = 100;
        san = 3;

        attackInterval = 2.2f;
        AttackTimes = 1;
        attackSpeedPlus = 1;
        attackDistance = 500;
        myState.aimCount = 1;
        attackAmount = 1;
        hasBetray = false;

        //特性
        roleName = "诗人";
        roleInfo = "精神+25%，防御力-20%";
        psyMul += 0.25f;
        defMul -= 0.2f;
        #region 弃用
        //mainProperty.Add("精神", "远法dps");
        //trait = gameObject.AddComponent<Sentimental>();
        #endregion

    }

    /// <summary>
    /// 身份
    /// </summary>
    //public override float psy { get { return base.psy * 1.25f; } set { base.psy = value; ; } }
    //public override float def { get { return base.def*0.8f; } set { base.def = value; } }


    List<GrowType> hasAddGrow = new List<GrowType>();
    public override string GrowText(GrowType type)
    {
        if ((!hasAddGrow.Contains(type)) &&(type == GrowType.psy))
        {
            hasAddGrow.Add(GrowType.psy);
            string it = "那天渐渐的黄昏，且阴的沉重，兼着那雨滴竹梢，更觉凄凉，黛玉不觉心有所感，亦不禁发于章句，遂成诗一首。";
            GameMgr.instance.draftUi.AddContent(it);
            return it;
        }
           


        return null;
    }

    public override string ShowText(AbstractCharacter otherChara)
    {
        if (otherChara != null)
            return "忽听外面人说：“林姑娘来了！'话犹未了，林黛玉已摇摇的走了进来，笑道，“嗳呦，我来的不巧了！”";
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
