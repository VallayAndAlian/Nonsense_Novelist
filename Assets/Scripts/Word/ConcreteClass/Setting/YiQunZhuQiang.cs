using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨��ͨ�á�����Ⱥ��ǽ
/// </summary>
public class YiQunZhuQiang : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "��Ⱥ��ǽ";
        res_name = "yiqunzhuqiang";
        info = "��ӵ���������+50%�����ϵ���������+100%";
        lables = new List<string> { "���" };
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
