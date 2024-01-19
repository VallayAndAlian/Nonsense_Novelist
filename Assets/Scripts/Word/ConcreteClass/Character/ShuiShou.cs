using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 税收
/// </summary>
class ShuiShou : AbstractCharacter
{
    override public void Awake()
    {
        base.Awake();
        characterID = 5;
        wordName = "税收";
        bookName = BookNameEnum.ElectronicGoal;
        gender = GenderEnum.noGender;
        hp =maxHp  = 220;
        atk = 5;
        def = 5;
        psy = 3;
        san = 3;
        mainProperty.Add("防御","中物T");
        trait=gameObject.AddComponent<ColdInexorability>();
        roleName = "垄断公司";
        attackInterval = 2.2f;
        attackDistance = 100;
        brief = "来自日常生活开销所产生的经济压力";
        description = "来自日常生活开销所产生的经济压力。";
    }

    AbstractCharacter[] aims;
    public override bool AttackA()
    {//代替平A
        if (myState.aim != null)
        {
            myState.character.CreateBullet(myState.aim.gameObject);
            if (myState.character.aAttackAudio != null)
            {
                myState.character.source.clip = myState.character.aAttackAudio;
                myState.character.source.Play();
            }
            myState.character.charaAnim.Play(AnimEnum.attack);
            aims = attackA.CalculateAgain(100, this);
            foreach (AbstractCharacter aim in aims)
            {//普通攻击为对所有敌人造成攻击力10%的伤害，附带攻击特效
                myState.character.CreateBullet(aim.gameObject);
               aim.CreateFloatWord(
                   attackA.UseMode(myState.character, myState.character.atk * 0.1f * (1 - myState.aim.def / (myState.aim.def + 20)), aim)
                   ,FloatWordColor.physics,true);
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
