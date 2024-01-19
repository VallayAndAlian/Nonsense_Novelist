using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class JiaGrandma_x : AbstractCharacter
{
    override public void Awake()
    {
        base.Awake();
        characterID = 10;
        gender = GenderEnum.girl;
        wordName = "贾母";
        bookName = BookNameEnum.HongLouMeng;
        nickname.Add( "史太君");
        trait=gameObject.AddComponent<Mercy>();
        hp=maxHp = 40;
        atk = 7;
        def = 5;
        psy = 7;
        san = 7;
        multipleCriticalStrike = 2;
        attackInterval = 2.2f;
        attackDistance = 500;
        description = "贾母，又称史老太君，贾府上下尊称她为“老太太”、“老祖宗”，是曹雪芹所著中国古典小说《红楼梦》中的主要角色之一，是贾府名义上的最高统治者，一生享尽荣华富贵。";
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
