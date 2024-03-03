using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色：失恋
/// </summary>
class ShiLian : AbstractCharacter
{
    override public void Awake()
    {
        base.Awake();

        //基础信息
        characterID = 12;
        wordName = "失恋";
        bookName = BookNameEnum.Salome;
        brief = "暂无介绍";
        description = "暂无介绍";

        //数值
        hp = maxHp = 100;
        atk = 3;
        def = 3;
        psy = 4;
        san = 4;

        attackInterval = 2.2f;
        AttackTimes = 1;
        attackSpeedPlus = 1;
        attackDistance = 500;
        myState.aimCount = 1;
        attackAmount = 1;
        hasBetray = false;

        //特性
        roleName = "负面情绪";
        roleInfo = "攻击有几率让对方沮丧";//普通攻击有20%几率附带“沮丧”状态，持续3s
        event_AttackA += UpsetAttackA;
    }
  
    /// <summary>
    /// 特性
    /// </summary>
    void UpsetAttackA()
    {
      
        for (int i = 0; i < myState.aim.Count; i++)
        {  
            if (Random.Range(1, 101) <= 20)
            {
                myState.aim[i].gameObject.AddComponent<Upset>().maxTime = 3;
            }
        }
    }

    private void OnDestroy()
    {
        event_AttackA -= UpsetAttackA;
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
