using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨��ͨ�á�����Ѫ�Ʒ�
/// </summary>
public class FangXueLiaoFa : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.GuiCai;
        settingName = "��Ѫ�Ʒ�";
        res_name = "fangxueliaofa";
        info = "ʹ�з���ɫ��ĳ�����״̬�ﵽ10��ʱ�������������������м���״̬��ÿ�������3%�����������˺�";
        lables = new List<string> { "�쳣" };
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
