using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �������
/// </summary>
public class JingCha : AbstractCharacter
{
    override public void Awake()
    {
        base.Awake();

        //��ȡ����
        MonsterExcelItem dataD = null;
        MonsterExcelItem data=null;
        for (int i=0;(i< PoolConfigData.instance.so.monsterDate.items.Length)&&(data==null);i++)
        {
            var _data = PoolConfigData.instance.so.monsterDate.items[i];
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
        print(GameMgr.instance.GetStage());
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
        roleName = "����";
        roleInfo = "ÿ3�ι���ʹ�Է����ʧȥ1����";//ÿ�����ι���ʹ�Է������һ������ʧȥ1����
        event_AttackA += AttackMore;

        //�Դ�����
        if ((data.word1 != null) && (data.word1 != ""))
        {
            var _s = System.Type.GetType(data.word1);
            if (_s != null)
            {
                var _ss=gameObject.AddComponent(_s);
                print(wordName + "���ϵ�" + (_ss as AbstractWord0).wordName + "�����ˣ�����Ч��");
            }
        }
        if ((data.word2 != null) && (data.word2 != ""))
        {
            var _s = System.Type.GetType(data.word2);
            if (_s != null)
            {
                var _ss = gameObject.AddComponent(_s);
                print(wordName + "���ϵ�" + (_ss as AbstractWord0).wordName + "�����ˣ�����Ч��");
            }
        }
        if ((data.word3 != null) && (data.word3 != ""))
        {
            var _s = System.Type.GetType(data.word3);
            if (_s != null)
            {
                var _ss = gameObject.AddComponent(_s);
                print(wordName + "���ϵ�" + (_ss as AbstractWord0).wordName + "�����ˣ�����Ч��");
            }
        }
        if ((data.word4 != null) && (data.word4 != ""))
        {
            var _s = System.Type.GetType(data.word4);
            if (_s != null)
            {
                var _ss = gameObject.AddComponent(_s);
                print(wordName + "���ϵ�" + (_ss as AbstractWord0).wordName + "�����ˣ�����Ч��");
            }
        }
    }
    int attackTimes=0;

    /// <summary>
    /// ���
    /// </summary>
    void AttackMore()
    {
        attackTimes += 1;
        if (attackTimes >= 3)
        {
            attackTimes = 0;
            for (int i = 0; i < myState.aim.Count; i++)
            {
                if (myState.aim[i].skills.Count == 0) return;
                int _random = Random.Range(0, myState.aim[i].skills.Count);
                myState.aim[i].skills[_random].CD -= 1;

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
            DraftMgr.instance.AddContent(it);
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
