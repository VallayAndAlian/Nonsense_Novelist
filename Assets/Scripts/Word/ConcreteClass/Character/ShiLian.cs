using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 失恋
/// </summary>
class ShiLian : AbstractCharacter
{
    override public void Awake()
    {
        base.Awake();
        characterID = 12;
        wordName = "失恋";
        bookName = BookNameEnum.Salome;
        gender = GenderEnum.noGender;
        hp = maxHp = 120;
        atk = 3;
        def = 4;
        psy = 5;
        san = 3;
        mainProperty.Add("精神", "中法dps");
        trait = gameObject.AddComponent<Vicious>();
        roleName = "负面情绪";
        attackInterval = 2.2f;
        attackDistance = 300;
    }
    private void Start()
    {
        attackState = GetComponent<AttackState>();
        Destroy(attackA);
        attackA = gameObject.AddComponent<DamageMode>();
    }

    AttackState attackState;
    AbstractCharacter[] aims;
    public override bool AttackA()
    {
        if (base.AttackA())
        {
            //普通攻击有20几率附带“沮丧”状态
            if (Random.Range(1, 101) <= 20)
                myState.aim.gameObject.AddComponent<Upset>().maxTime = 5;
            return true;
        }
        return false;
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
