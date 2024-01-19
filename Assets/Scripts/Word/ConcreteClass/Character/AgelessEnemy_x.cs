using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 宿敌（暂时弃用）
/// </summary>
class AgelessEnemy_x : AbstractCharacter
{
    override public void Awake()
    {
        base.Awake();
        characterID = -1;
        gender = GenderEnum.noGender;
        wordName = "宿敌";
        bookName = BookNameEnum.allBooks;
        description = "长期与你过不去，并且不知为何总是会不断遇到对方";
        trait = gameObject.AddComponent<Pride>();
        hp=maxHp = 70;
        atk = 15;
        def = 2;
        psy = 7;
        san = 2;
        attackInterval = 2.2f;
        attackDistance = 600;
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
