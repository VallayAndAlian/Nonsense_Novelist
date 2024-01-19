using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 刘姥姥
/// </summary>
class LiuGrandma : AbstractCharacter
{
    override public void Awake()
    {
        base.Awake();
        characterID = 3;
        gender = GenderEnum.girl;
        wordName = "刘姥姥";
        bookName = BookNameEnum.HongLouMeng;
        nickname.Add( "母蝗虫");
        trait = gameObject.AddComponent<Enthusiasm>();
        hp =maxHp = 70;
        atk = 5;
        def = 5;
        psy = 5;
        san = 10;
        multipleCriticalStrike = 2;
        attackInterval = 2.2f;
        attackDistance = 200;
        description = "刘姥姥，是曹雪芹所著中国古典文学名著《红楼梦》中人物。是一位来自乡下贫农家庭的谙于世故的老婆婆，见证了贾府兴衰荣辱的全过程。";
    }

    public override string ShowText(AbstractCharacter otherChara)
    {
        return "";
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
}
