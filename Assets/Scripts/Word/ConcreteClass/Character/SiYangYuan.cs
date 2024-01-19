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
        base.Awake();
        characterID = 11;
        wordName = "饲养员";
        bookName = BookNameEnum.ZooManual;
        gender = GenderEnum.noGender;
        hp =maxHp  = 100;
        atk = 0;
        def = 5;
        psy = 3;
        san = 5;
        trait=gameObject.AddComponent<Mercy>();
        roleName = "饲养员";
        attackInterval = 2.2f;
        attackDistance = 500;

        Destroy(attackA);
        attackA = gameObject.AddComponent<CureMode>();
        description = "暂无文案";
    }

    AbstractCharacter[] aims;
    public override bool AttackA()
    {//代替平A
        if (hp <= 0) return false;
        if (myState.aim != null)
        {
            myState.character.CreateBullet(myState.aim.gameObject);
            if (myState.character.aAttackAudio != null)
            {
                myState.character.source.clip = myState.character.aAttackAudio;
                myState.character.source.Play();
            }
            myState.character.charaAnim.Play(AnimEnum.attack);
            //普通攻击目标为血量百分比最低的队友，恢复120%意志的血量，以及“亢奋”状态
            myState.aim.CreateFloatWord(
            attackA.UseMode(myState.character, san * sanMul * 1.2f, myState.aim)
            ,FloatWordColor.heal,false);
            myState.aim.gameObject.AddComponent<KangFen>().maxTime = 5;
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
