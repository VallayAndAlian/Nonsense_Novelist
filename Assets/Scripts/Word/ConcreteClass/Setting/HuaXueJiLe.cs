using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨��ͨ�á�����ѧ����
/// </summary>
public class HuaXueJiLe : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.QiaoSi;
        settingName = "��ѧ����";
        res_name = "huaxuejile";
        info = "�ҷ������족���ٵ���־��ת��Ϊ����";
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
