using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨����ɫ�����ù�ʳ��
/// </summary>
public class QiaoGuShiSui : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "�ù�ʳ��";
        res_name = "qiaogushisui";
        info = "¢�Ϲ�˾��ɼ����빥����Ч�˺���15%���ָ���������ӵ�����";
        lables = new List<string> { "��ɫ"};
        hasAdd = false;
        Init();
    }
    public override void Init()
    {
        if (hasAdd) return;

        chara = CharacterManager.instance.gameObject.GetComponentInChildren<LongDuanGongSi>();
        if (chara != null)
        {
            chara.event_AddBuff += Effect;
        }
        hasAdd = true;
    }
    void Effect(AbstractBuff buff)
    {


    }
}
