using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨��ͨ�á����Ứ�ۻ�
/// </summary>
public class ZangHuaJuHun : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "�Ứ�ۻ�";
        res_name = "zanghuajuhun";
        info = "���ҷ���ɫ����ʱ������50����ʹ�临��";
        lables = new List<string> { "����","����" };
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
