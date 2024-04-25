using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 怪物：警察
/// </summary>
public class JingCha : AbstractCharacter
{
    override public void Awake()
    {
        base.Awake();

        //基础信息
        characterID = 112;
        wordName = "警察";
        bookName = BookNameEnum.allBooks;
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
        roleName = "射手";
        roleInfo = "每3次攻击使对方随机失去1能量";//每第三次攻击使对方的随机一个动词失去1能量
        event_AttackA += AttackMore;
    }
    int attackTimes=0;

    /// <summary>
    /// 身份
    /// </summary>
    void AttackMore()
    {
        attackTimes += 1;
        if (attackTimes >= 3)
        {
            attackTimes = 0;
            for (int i = 0; i < myState.aim.Count; i++)
            {
                if (myState.aim[i].skills.Count == 0) return;
                int _random = Random.Range(0, myState.aim[i].skills.Count);
                myState.aim[i].skills[_random].CD -= 1;

            }


        }
    }

    private void OnDestroy()
    {
        event_AttackA -= AttackMore;
    }



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
