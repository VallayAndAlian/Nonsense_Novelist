using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色：老鼠
/// </summary>
class Rat : AbstractCharacter
{
    override public void Awake()
    {
        base.Awake();

        //基础信息
        characterID = 6;
        wordName = "老鼠";
        bookName = BookNameEnum.allBooks;
        brief = "肮脏且会偷窃物品的老鼠";
        description = "暂无文案";

        //数值
        hp = maxHp = 100;
        atk = 3;
        def = 5;
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
        roleName = "小偷";
        roleInfo = "攻击可以偷取对方的名词";
       
        event_AttackA += GetNouns;
    }

    /// <summary>
    /// 身份
    /// </summary>
    int attackCount = 0;
    void GetNouns()
    { //每9次攻击，下次攻击偷取攻击目标的一个随机名词
        attackCount++;
        if (attackCount != 10) return;

        for (int i = 0; i < myState.aim.Count; i++)
        {
            int count = myState.aim[i].GetComponents<AbstractItems>().Length;
            if (count != 0)
            {
                int _random = Random.Range(0, count);
                AbstractItems _item = myState.aim[i].GetComponents<AbstractItems>()[_random];
                AbstractItems noun = this.gameObject.AddComponent(_item.GetType()) as AbstractItems;
                CreateFloatWord( noun.wordName, FloatWordColor.getWord, false);
                noun.UseItem(this.gameObject.GetComponent<AbstractCharacter>());
                Destroy(_item);
            }
        }
        attackCount = 0;


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
        return otherChara+"出场文本";
    }

    public override string CriticalText(AbstractCharacter otherChara)
    {
        return "";
    }

    public override string LowHPText()
    {
        return "";
    }
    public override string DieText()
    {
        return "";
    }
    private void OnDestroy()
    {
        event_AttackA -= GetNouns;
    }
}
