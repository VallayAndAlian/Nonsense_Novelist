using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 饲养员
/// </summary>
class SiYangYuan : AbstractCharacter
{
    override public void Awake()
    {
        isNaiMa = true;

        base.Awake();

        //基础信息
        characterID = 11;
        wordName = "饲养员";
        bookName = BookNameEnum.ZooManual;
        brief = "暂无介绍";
        description = "暂无介绍";

        //数值
        hp = maxHp = 100;
        atk = 0;
        def = 5;
        psy = 3;
        san = 6;

        attackInterval = 2.2f;
        AttackTimes = 1;
        attackSpeedPlus = 1;
        attackDistance = 500;
        myState.aimCount = 1;
        attackAmount = 1;
        hasBetray = false;

        //特性
        roleName = "饲养员";
        roleInfo = "普通攻击的治疗，附带5s亢奋";//普通攻击有20%几率附带“亢奋”状态，持续5s

    }

    AbstractCharacter[] aims;
    public override bool AttackA()
    {
        //if (hp <= 0) return false;
        if (myState.aim != null)
        {
          
            if (myState.character.aAttackAudio != null)
            {
                myState.character.source.clip = myState.character.aAttackAudio;
                myState.character.source.Play();
            }
            myState.character.charaAnim.Play(AnimEnum.attack);



            for (int i = 0; i < myState.aim.Count; i++)
            {
                attackA.UseMode(AttackType.heal, san * sanMul * 1.2f, myState.character, myState.aim[i], true, 0);
                if (Random.Range(1, 101) <= 20)
                {
                    myState.aim[i].gameObject.AddComponent<KangFen>().maxTime = 5;
                }
            }

            //执行外部委托
            if (event_AttackA != null)
                event_AttackA();

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
