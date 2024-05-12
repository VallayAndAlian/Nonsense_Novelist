using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���������΢��
/// </summary>
public class FangSheXingWeiCheng : AbstractCharacter
{
    override public void Awake()
    {
        base.Awake();

        //������Ϣ
        characterID = 110;
        wordName = "������΢��";
        bookName = BookNameEnum.ElectronicGoal;
        brief = "�����İ�";
        description = "�����İ�";


        //��ȡ����
        MonsterExcelItem dataD = null;
        MonsterExcelItem data = null;
        for (int i = 0; (i < GameMgr.instance.monsterDate.items.Length) && (data == null); i++)
        {
            var _data = GameMgr.instance.monsterDate.items[i];
            if (_data.Mid == characterID)
            {
                if (dataD == null) dataD = _data;
                if (_data.name == GameMgr.instance.GetStage())
                {
                    data = _data;
                }
            }
        }
        if (data == null)
            data = dataD;
        if (data == null)
            return;

        print("��ȡ�ɹ���" + wordName + data.name);

        //��ֵ
        hp = maxHp = data.hp;
        atk = data.atk;
        def = data.def;
        psy = data.psy;
        san = data.san;

        attackInterval = 2.2f;
        AttackTimes = 1;
        attackSpeedPlus = 1;
        attackDistance = 500;
        myState.aimCount = 1;
        attackAmount = 1;
        hasBetray = false;

        //����
        roleName = "̹��";
        roleInfo = "20%���ʶԹ�����ʩ���ж�";//

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
            if (_random <= 20)
            {
                var _buff=myState.aim[i].gameObject.AddComponent<Toxic>();
                myState.aim[i].AddBuff(_buff);
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
