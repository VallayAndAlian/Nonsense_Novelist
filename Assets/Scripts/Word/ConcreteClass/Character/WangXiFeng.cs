using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 王熙凤
/// </summary>
class WangXiFeng : AbstractCharacter
{
    override public void Awake()
    {
        base.Awake();
        characterID = 2;
        wordName = "王熙凤";
        bookName = BookNameEnum.HongLouMeng;
        gender = GenderEnum.girl;
        hp = maxHp = 150;
        atk = 5;
        def = 4;
        psy = 3;
        san = 3;
        mainProperty.Add("攻击", "近物dps");
        trait = gameObject.AddComponent<Spicy>();
        roleName = "大家长";
        attackInterval = 2.2f;
        attackDistance = 200;
        brief = "《红楼梦》中一位泼辣且极具备能力的女人。";
        description = "王熙凤，曹雪芹所著中国古典小说《红楼梦》中的人物，\n金陵十二钗之一，贾琏的妻子。\n在贾府掌握实权，为人心狠手辣，八面玲珑，敢爱敢恨，做事决绝。" +
            "\n因其深爱丈夫贾琏，故而十分善妒，暗中算计害死尤二姐。\n王熙凤在被休后王家不容她,在监牢里血崩病发,流尽鲜血而死。";
    }

    /// <summary>
    /// 身份
    /// </summary>
    public override float atk { get { return base.atk +5; } set { base.atk = value; } }
    public override float def { get { return base.def + 5; } set { base.def = value; } }
    public override float san { get { return base.san + 5; } set { base.san = value; } }
    public override float psy { get { return base.psy  + 5; } set { base.psy  = value; } }

    public override string ShowText(AbstractCharacter otherChara)
    {
        if (otherChara != null)
            return "只见传来一阵高亢的笑声“" + otherChara.wordName + "，是我来迟了”。语罢便走出了一个浓妆美艳的少妇，正是王熙凤。";
        else
            return null;
    }

    public override string CriticalText(AbstractCharacter otherChara)
    {
        if (otherChara != null)
            return "王熙凤对着"+otherChara .wordName+"狠狠地一巴掌“你这癞狗扶不上墙的种子！”,";
        else
            return null;
    }
    public override string LowHPText()
    {
        return "王熙凤一生中操劳太过，因又气恼而气血上涌而引起血崩，此时身体变得十分虚弱。";
    }

    public override string DieText()
    {
        return "面色惨白的王熙凤支持不住倒了下去。";
    }

}
