
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ��ӣ���Ģ��
/// </summary>
/// 

public class CS_DuMoGu : ServantAbstract
{
    static public string s_description = "�Թ��������1*<sprite name=\"psy\">��ħ���˺���3s<color=#dd7d0e>���ж���</color>";
    static public string s_wordName = "��ӣ���Ģ��";

    override public void Awake()
    {
        base.Awake();
       
        this.event_BeAttack += DoToAttacker;


    }

    void DoToAttacker(float _value,AbstractCharacter _attacker)
    {
        _attacker.BeAttack(AttackType.psy, 1 * this.atk * atkMul, true, 0, this);
        var buff=_attacker.gameObject.AddComponent<Upset>();buff.maxTime =3;
    }

    private void OnDestroy()
    {
        this.event_BeAttack -= DoToAttacker;
        masterNow.DeleteServant(this.gameObject);

    }

    public override string ShowText(AbstractCharacter otherChara)
    {
        if (otherChara != null)
            return otherChara.wordName + "���ѿ�������һ�����ã�ϸ�����ݣ�ֻ������㣬����΢΢���о�ʱ��毻���ˮ���ж������������磬" + otherChara.wordName + "Ц������������ã����������ġ�";
        else
            return null;
    }
    public override string CriticalText(AbstractCharacter otherChara)
    {
        if (otherChara != null)
            return "���Ҿ�֪�������˲���ʣ�µ�Ҳ�����ҡ�������������һ�仨�꣬��" + otherChara.wordName + "��ȥ";
        else
            return null;
    }

    public override string LowHPText()
    {
        return "�������Ů��Ϣ���������ϻ���ա����㽫һ�����ӣ�һ��ʫ��پ��ڻ����С�";
    }
    public override string DieText()
    {
        return "�����񡭱�����á���������û˵��������˫�ۡ�";
    }


}

