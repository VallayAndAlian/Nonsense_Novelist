using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨��ͨ�á�����������
/// </summary>
public class FuFuDeZheng : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "��������";
        res_name = "fufudezheng";
        info = "�������ѷ���ͬʱ�����3����ͬ����ĸ���״̬������ȫ������";
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
