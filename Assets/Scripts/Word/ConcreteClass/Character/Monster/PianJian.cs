using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ƫ��
/// </summary>
public class PianJian : AbstractCharacter
{
    override public void Awake()
    {
        base.Awake();

        //������Ϣ
        characterID = 113;
        wordName = "ƫ��";
        bookName = BookNameEnum.allBooks;
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
        roleName = "��ʦ";
        roleInfo = "������30%�����Ƴ��Է�һ������";//30%���ʹ����Ƴ�һ������
        event_AttackA += AttackMore;
    }

    /// <summary>
    /// ���
    /// </summary>
    void AttackMore()
    {
        for (int i = 0; i < myState.aim.Count; i++)
        {
            int _random = Random.Range(0, 100);
            if (_random <= 30)
            {
                myState.aim[i].DeleteGoodBuff(1);
            }
        }
    }

    private void OnDestroy()
    {
        event_AttackA -= AttackMore;
    }




    List<GrowType> hasAddGrow = new List<GrowType>();
    public override string GrowText(GrowType type)
    {
        if ((!hasAddGrow.Contains(type)) && (type == GrowType.psy))
        {
            hasAddGrow.Add(GrowType.psy);
            string it = "���콥���Ļƻ裬�����ĳ��أ�������������ң��������������񲻾��������У��಻�������¾䣬���ʫһ�ס�";
            GameMgr.instance.draftUi.AddContent(it);
            return it;
        }



        return null;
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
