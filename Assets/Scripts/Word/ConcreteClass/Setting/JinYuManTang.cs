using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨����������
/// </summary>
public class JinYuManTang : AbstractSetting
{
    AbstractCharacter chara;
    public override void Start()
    {
        base.Start();

        level = SettingLevel.PingYong;
        name = "��������";
        info = "�������õ����ʺ����ݴʣ���25%���������������";
        lables = new List<string> { "��ɫ"};

        hasAdd = false;

        Init();


    }
    public override void Init()
    {
        if (hasAdd) return;

        chara = CharacterManager.instance.gameObject.GetComponentInChildren<WangXiFeng>();
        if (chara != null)
        {
            chara.event_AddAdj += EffectA;
            chara.event_AddNoun += EffectB;
        }
        hasAdd = true;
    }
    void EffectA(AbstractAdjectives _av)
    {
        //��ӵ�ж����е�һ��������1�������ޣ�����Ϊ1
        

    }
    void EffectB(AbstractItems _av)
    {
        //��ӵ�ж����е�һ��������1�������ޣ�����Ϊ1


    }
    private void OnDestroy()
    {
        if (!hasAdd) return;
        chara.event_AddAdj -= EffectA;
        chara.event_AddNoun -= EffectB;
    }
}
