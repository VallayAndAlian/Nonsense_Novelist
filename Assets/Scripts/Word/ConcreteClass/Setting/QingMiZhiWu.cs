using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨��ͨ�á�������֮��
/// </summary>
public class QingMiZhiWu : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.QiaoSi;
        settingName = "����֮��";
        res_name = "qingmizhiwu";
        info = "�з����м���״̬��ʱ��+1s������+2s";
        lables = new List<string> { "�쳣","����" };
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
