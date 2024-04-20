using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 怪物：放射性微尘
/// </summary>
public class SaiBoFengZi : AbstractCharacter
{
    override public void Awake()
    {
        base.Awake();

        //基础信息
        characterID = 7;
        wordName = "赛博疯子";
        bookName = BookNameEnum.ElectronicGoal;
        brief = "暂无文案";
        description = "暂无文案";

        //数值
        hp = maxHp = 30;
        atk = 6;
        def = 20;
        psy = 6;
        san = 15;

        attackInterval = 2.2f;
        AttackTimes = 1;
        attackSpeedPlus = 1;
        attackDistance = 500;
        myState.aimCount = 1;
        attackAmount = 1;
        hasBetray = false;

        //特性
        roleName = "战士";
        roleInfo = "不分敌我地快速攻击";//攻击间隔1s，常驻“疯狂”状态，不会被移除，每次不分敌我攻击随机目标
        cureHpRate += 0.05f;

    }




    public override string ShowText(AbstractCharacter otherChara)
    {
        if (otherChara != null)
            return "阿努比斯出场文本";
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
