using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨��ͨ�á������ⷺ��
/// </summary>
public class AiYiFanLan : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.GuiCai;
        settingName = "���ⷺ��";
        res_name = "aiyifanlan";
        info = "�з����Խ����󣬶������һ�δ���������3s";
        lables = new List<string> {"����" };
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
