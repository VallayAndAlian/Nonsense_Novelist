using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨��ͨ�á�����ľ�괺
/// </summary>
public class KuMuFengChun : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.QiaoSi;
        settingName = "��ľ�괺";
        res_name = "kumufengchun";
        info = "ȫ����ɫÿ�ɹ�����һ�ָ���״̬���������10������";
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
