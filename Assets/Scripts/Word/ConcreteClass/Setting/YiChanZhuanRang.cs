using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨��ͨ�á����Ų�ת��
/// </summary>
public class YiChanZhuanRang : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.QiaoSi;
        settingName = "�Ų�ת��";
        res_name = "yichanzhuanrang";
        info = "�ҷ���ɫ����ʱ�������Я����3�����ʸ��Ƹ�����";
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
