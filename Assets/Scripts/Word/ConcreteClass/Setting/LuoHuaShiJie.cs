using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨��ͨ�á����仨ʱ��
/// </summary>
public class LuoHuaShiJie : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "�仨ʱ��";
        res_name = "luohuashijie";
        info = "ÿ2s�������ɫ���1����";
        lables = new List<string> { "����" };
        hasAdd = false;
       
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
