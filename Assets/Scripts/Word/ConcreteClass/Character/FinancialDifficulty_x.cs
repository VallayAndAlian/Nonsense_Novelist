using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 经济压力
/// </summary>
class FinancialDifficulty_x : AbstractCharacter
{
    override public void Awake()
    {
        base.Awake();
        characterID = 7;
        gender = GenderEnum.noGender;
        wordName = "经济压力";
        bookName = BookNameEnum.allBooks;
        trait=gameObject.AddComponent<NullTrait>();
        hp=maxHp = 100;
        atk = 10;
        def = 12;
        psy = 0;
        san = 2;
        multipleCriticalStrike = 2;
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
