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
        //isNaiMa = true;

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
        roleName = "喂食";
        roleInfo = "普通攻击有概率朝队友扔食物，恢复100%意志并赋予“亢奋”5s";//普通攻击有30%几率附带“亢奋”状态，持续5s
        event_AttackA += ThrowFood;
    }

    void ThrowFood()
    {
        int _r = Random.Range(0, 100);
        if (_r < 30)
        {
            var chara = CharacterManager.instance.GetFriend(this.camp);
            if (chara[0] != null)
            {
                var _b=chara[0].gameObject.AddComponent<KangFen>();
                _b.maxTime = 5f;
                chara[0].BeCure(chara[0].san * chara[0].sanMul * 1, true, 0, this);
            }
        }
    }

    //AbstractCharacter[] aims;
    //public override bool AttackA()
    //{
    //    //if (hp <= 0) return false;
    //    if (myState.aim != null)
    //    {
          
    //        if (myState.character.aAttackAudio != null)
    //        {
    //            myState.character.source.clip = myState.character.aAttackAudio;
    //            myState.character.source.Play();
    //        }
    //        myState.character.charaAnim.Play(AnimEnum.attack);



    //        for (int i = 0; i < myState.aim.Count; i++)
    //        {
    //            attackA.UseMode(AttackType.heal, san * sanMul * 1.2f, myState.character, myState.aim[i], true, 0);
    //            if (Random.Range(1, 101) <= 20)
    //            {
    //                myState.aim[i].gameObject.AddComponent<KangFen>().maxTime = 5;
    //            }
    //        }

    //        //执行外部委托
    //        if (event_AttackA != null)
    //            event_AttackA();

    //        return true;
    //    }
    //    return false;
    //}


    List<GrowType> hasAddGrow = new List<GrowType>();
    public override string GrowText(GrowType type)
    {
        if ((!hasAddGrow.Contains(type)) && (type == GrowType.psy))
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
        return otherChara + "出场文本";
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
