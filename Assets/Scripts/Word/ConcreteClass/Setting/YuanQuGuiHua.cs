using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨��ͨ�á���԰���滮
/// </summary>
public class YuanQuGuiHua : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.QiaoSi;
        settingName = "԰���滮";
        res_name = "yuanquguihua";
        info = "ÿ��3���ҷ���Ӻϳ�Ϊ������ʱ������������һ�����";
        lables = new List<string> { "���" };
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
