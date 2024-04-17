using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨����������
/// </summary>
public class JinYuManTang : AbstractSetting
{
    AbstractCharacter chara;
    public override void Awake()
    {
        base.Awake();

        level = SettingLevel.QiaoSi;
        settingName = "��������";
        res_name = "jinyumantang";
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
        //��ӵ�����ݴ��е�һ��������1�������ޣ�����Ϊ1
        int number = Random.Range(1, 5);
        if (number == 1) { chara.gameObject.AddComponent(_av.GetType()); }
        else return;

    }
    void EffectB(AbstractItems _av)
    {
        //��ӵ�������е�һ��������1�������ޣ�����Ϊ1
        int number = Random.Range(1, 5);
        if (number == 1) { chara.gameObject.AddComponent(_av.GetType()); }
        else return;

    }
    private void OnDestroy()
    {
        if (!hasAdd) return;
        chara.event_AddAdj -= EffectA;
        chara.event_AddNoun -= EffectB;
    }
}
