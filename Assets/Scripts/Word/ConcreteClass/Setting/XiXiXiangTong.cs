using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨��ͨ�á���ϢϢ��ͨ
/// </summary>
public class XiXiXiangTong : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "ϢϢ��ͨ";
        res_name = "xixixiangtong";
        info = "ͨ�д����ڳ����ڵ�ʱ������10s";
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
