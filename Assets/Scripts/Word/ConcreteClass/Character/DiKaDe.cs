using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 狄卡德
/// </summary>
class DiKaDe : AbstractCharacter
{
    override public void Awake()
    {
        base.Awake();

        //基础信息
        characterID = 9;
        wordName = "狄卡德";
        bookName = BookNameEnum.ElectronicGoal;
        brief = "暂无介绍";
        description = "暂无介绍";

        //数值
        hp = maxHp = 100;
        atk = 5;
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
        roleName = "银翼杀手";
        roleInfo = "攻击额外造成对方最大生命值3%的伤害";//普通攻击额外造成3%最大生命值伤害

        event_AttackA += AttackMore;
    }

    /// <summary>
    /// 身份
    /// </summary>
    void AttackMore()
    {
        for (int i = 0; i < myState.aim.Count; i++)
        {
            myState.aim[i].BeAttack(AttackType.dir, myState.aim[i].maxHp * 0.03f, true, 0, this) ;
        }
    }

    private void OnDestroy()
    {
        event_AttackA-= AttackMore;
    }



    #region 文本
  
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
    
    #endregion

}
