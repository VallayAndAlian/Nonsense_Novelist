using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨��ͨ�á���ӡ��ЧӦ
/// </summary>
public class YinKeXiaoYing : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.QiaoSi;
        settingName = "ӡ��ЧӦ";
        res_name = "yinkexiaoying";
        info = "�����ͷż���ʱ�������50%����׷��һ�ι���������������Ч";
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
