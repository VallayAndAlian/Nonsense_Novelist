using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨��ͨ�á���ͬ������
/// </summary>
public class TongShengGOngSi : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.QiaoSi;
        settingName = "ͬ������";
        res_name = "tongshenggongsi";
        info = "�ҷ���ɫ���Թ������ڶ��ѵĸ������";
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
