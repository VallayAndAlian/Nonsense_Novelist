using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨��ͨ�á�����ת�þ�
/// </summary>
public class AiZhuanJiuJue : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "��ת�þ�";
        res_name = "aizhuanjiujue";
        info = "�ҷ���ɫ����ʱ�����2���з���ɫ���10s����ɥ��";
        lables = new List<string> { "����" };
        hasAdd = false;
        Init();
    }
    public override void Init()
    {
        if (hasAdd) return;
        

        hasAdd = true;
    }
    void Effect(AbstractCharacter ac)
    {
        
    }

    private void OnDestroy()
    {
        if (!hasAdd) return;
    }
}
