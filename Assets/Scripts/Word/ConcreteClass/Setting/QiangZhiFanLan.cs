using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨��ͨ�á���ǹ֧����
/// </summary>
public class QiangZhiFanLan : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "ǹ֧����";
        res_name = "qiangzhifanlan";
        info = "ȫ����ɫÿʹ��6�����ʣ������ͷ�һ��ǹ��";
        lables = new List<string> { "��Ƶ" };
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
