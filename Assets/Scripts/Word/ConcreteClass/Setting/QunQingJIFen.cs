using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨��ͨ�á���Ⱥ�鼤��
/// </summary>
public class QunQingJIFen : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.GuiCai;
        settingName = "Ⱥ�鼤��";
        res_name = "qunqingjifen";
        info = "�ҷ���ɫ����ʱ�������ѷ����ȫ����+5";
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
