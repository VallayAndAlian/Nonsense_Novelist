using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨��ͨ�á����ǻۺ���
/// </summary>
public class ZhiHuiHeXin : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.GuiCai;
        settingName = "�ǻۺ���";
        res_name = "zhihuihexin";
        info = "ȫ��ÿ�㡰׿Խ���ܡ�ʹ��ɫ����+10%";
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
