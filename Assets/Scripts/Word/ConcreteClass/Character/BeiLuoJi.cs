using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色：贝洛姬·姬妮
/// </summary>
class BeiLuoJi : AbstractCharacter
{
    override public void Awake()
    {
        isNaiMa = true;

        base.Awake();

        //基础信息
        characterID = 10;
        wordName = "贝洛姬·姬妮";
        bookName = BookNameEnum.PHXTwist;
        brief = "暂无介绍";
        description = "暂无介绍";

        //数值
        hp = maxHp = 100;
        atk = 0;
        def = 4;
        psy = 3;
        san = 7;

        attackInterval = 2.2f;
        AttackTimes = 1;
        attackSpeedPlus = 1;
        attackDistance = 500;
        myState.aimCount = 1;
        attackAmount = 1;
        hasBetray = false;

        //特性
        roleName = "蚁后";
        roleInfo = "同时治疗三个友方";//普通攻击目标为3个低血量的队友，恢复70%意志的血量，不附带攻击攻击特效
        myState.aimCount = 3;

    }

    AbstractCharacter[] aims;

    int aimCount = 0;
    public override bool AttackA()
    {//代替平A
        aimCount = 0;
        if (myState.aim != null)
        {
            if (myState.character.aAttackAudio != null)
            {
                myState.character.source.clip = myState.character.aAttackAudio;
                myState.character.source.Play();
            }
            myState.character.charaAnim.Play(AnimEnum.attack);


            aims = attackA.CalculateAgain(100, this);

            foreach (AbstractCharacter aim in aims)
            {//普通攻击目标为所有队友，恢复70%意志的血量，不附带攻击攻击特效
                if (aim.myState.nowState == aim.myState.allState.Find(p => p.id == AI.StateID.dead) || (aimCount >= 3))
                {}
                else
                {
                    aimCount += 1;
                    attackA.UseMode(AttackType.heal, san *sanMul* 0.7f, myState.character, aim, true, 0);
                    myState.character.CreateBullet(aim.gameObject);
                }


             
            }
            return true;    
        }
        return false;

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
