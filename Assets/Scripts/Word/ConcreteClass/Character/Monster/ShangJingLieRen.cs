using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����ͽ�����
/// </summary>
public class ShangJingLieRen : AbstractCharacter
{
    override public void Awake()
    {
        base.Awake();

        //������Ϣ
        characterID = 114;
        wordName = "�ͽ�����";
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
        roleName = "�̿�";
        roleInfo = "����ʱ���Է�ÿ��һ�������˺�+1";//����ʱ���Է�ÿ��һ�������˺�+1
       

        event_AttackA += AttackMore;
    }


    void AttackMore()
    {
        for (int i = 0; i < myState.aim.Count; i++)
        {
            int _count = myState.aim[i].GetComponents<AbstractItems>().Length;
            myState.aim[i].BeAttack(AttackType.dir, 1* _count, true, 0, this);
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
