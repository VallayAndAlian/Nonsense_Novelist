using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨��ͨ�á�������ϵͳ
/// </summary>
public class MianYiXiTong : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "����ϵͳ";
        res_name = "mianyixitong";
        info = "���ҷ���ɫ�ܵ�����ʱ����20%�ĸ��ʾ���1�㸺��״̬";
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
