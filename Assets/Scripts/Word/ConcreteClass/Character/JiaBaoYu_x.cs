using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 贾宝玉
/// </summary>
class JiaBaoYu_x : AbstractCharacter
{
    override public void Awake()
    {
        base.Awake();
        characterID = 3;
        wordName = "贾宝玉";
        bookName = BookNameEnum.HongLouMeng;
        gender = GenderEnum.girl;
        hp = maxHp = 80;
        atk = 3;
        def = 3;
        psy = 5;
        san = 3;
        mainProperty.Add("精神", "远法dps");
        trait = gameObject.AddComponent<Sentimental>();
        attackInterval = 2.2f;
        attackDistance = 500;
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
