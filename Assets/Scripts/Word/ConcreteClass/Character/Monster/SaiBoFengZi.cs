using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���������΢��
/// </summary>
public class SaiBoFengZi : AbstractCharacter
{
    override public void Awake()
    {
        base.Awake();

        //������Ϣ
        characterID = 7;
        wordName = "��������";
        bookName = BookNameEnum.ElectronicGoal;
        brief = "�����İ�";
        description = "�����İ�";

        //��ֵ
        hp = maxHp = 30;
        atk = 6;
        def = 20;
        psy = 6;
        san = 15;

        attackInterval = 2.2f;
        AttackTimes = 1;
        attackSpeedPlus = 1;
        attackDistance = 500;
        myState.aimCount = 1;
        attackAmount = 1;
        hasBetray = false;

        //����
        roleName = "սʿ";
        roleInfo = "���ֵ��ҵؿ��ٹ���";//�������1s����פ�����״̬�����ᱻ�Ƴ���ÿ�β��ֵ��ҹ������Ŀ��
        cureHpRate += 0.05f;

    }




    public override string ShowText(AbstractCharacter otherChara)
    {
        if (otherChara != null)
            return "��Ŭ��˹�����ı�";
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
