using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨��ͨ�á�����˼Ȫӿ
/// </summary>
public class CaiSIQuanYong : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "��˼Ȫӿ";
        res_name = "caisiquanyong";
        info = "�ҷ������ܡ��ܹ���ȱ��4������ԭ��Ϊ5���Ķ��ʳ���";
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
