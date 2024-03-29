using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 阿努比斯
/// </summary>
class Anubis : AbstractCharacter
{
    float hpadd = 0;
    float timer = 0;
    override public void Awake()
    {
 
        base.Awake();

        //基础信息
        characterID = 7;
        wordName = "阿努比斯";
        bookName = BookNameEnum.EgyptMyth;
        brief = "暂无文案";
        description = "暂无文案";

        //数值
        hp = maxHp = 160;
        atk = 3;
        def = 3;
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
        roleName = "死神";
        roleInfo = "恢复+5%";//每10秒，恢复5%最大生命值
        cureHpRate +=0.05f;

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
