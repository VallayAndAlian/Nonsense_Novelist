using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨��ͨ�á���ȫ����
/// </summary>
public class QuanBoDuan : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.QiaoSi;
        settingName = "ȫ����";
        res_name = "quanboduan";
        info = "����Ĳ������㣬�Ӽ����ѷ����񣬸�Ϊ����ȫ���Ĺ���";
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
