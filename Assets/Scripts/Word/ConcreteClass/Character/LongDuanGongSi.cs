using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 角色：垄断公司
/// </summary>
class LongDuanGongSi : AbstractCharacter
{
    override public void Awake()
    {
        base.Awake();

        //基础信息
        characterID = 5;
        wordName = "垄断公司";
        bookName = BookNameEnum.ElectronicGoal;
        brief = "暂无";
        description = "暂无";

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
        mainProperty.Add("精神", "远法dps");
        trait = gameObject.AddComponent<Sentimental>();
        roleName = "税收";
        roleInfo = "普通攻击造成伤害的30%，转化为生命";//转化为自身与随从的生命

        event_AttackA += RoleFunction;

    }



    void RoleFunction()//普通攻击造成伤害的30%，转化为生命
    {
     
        BeCure(atk * atkMul * attackAmount * 0.3f, true, 0);
        for (int i = 0; i < servants.Count; i++)
        {
            servants[i].GetComponent<AbstractCharacter>().BeCure(atk * atkMul * attackAmount * 0.3f, true, 0);
        }

    }

    private void OnDestroy()
    {
        event_AttackA -= RoleFunction;

    }



    #region 文本
 
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
    
    #endregion

}
