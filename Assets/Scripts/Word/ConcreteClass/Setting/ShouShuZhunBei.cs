using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨��ͨ�á�������׼��
/// </summary>
public class ShouShuZhunBei : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "����׼��";
        res_name = "shoushuzhunbei";
        info = "��ɫÿ2���ͷŶ��ʣ��������´ι��������ʵ�˺�";
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
