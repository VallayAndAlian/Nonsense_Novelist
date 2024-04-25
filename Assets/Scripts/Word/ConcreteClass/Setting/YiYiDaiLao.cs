using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨�����ܡ������ݴ���
/// </summary>
public class YiYiDaiLao : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "���ݴ���";
        res_name = "yiyidailao";
        info = "��ɫÿ��ȡ1�������ָ�3%�������";
        lables = new List<string> {  };
        hasAdd = false;
      
    }
    public override void Init()
    {
        if (hasAdd) return;

        foreach (var _c in CharacterManager.instance.GetFriend(camp))
        {
            _c.OnEnergyFull += Effect; //ÿ�λ��һ���������ʱ�򣬶�ִ�д˺���
        }
        hasAdd = true;
    }
    float record = 0;//��ʱ����attackAmountϵ��
    
    //ÿ�λ��һ���������ʱ�򣬶�ִ�д˺���
    void Effect(AbstractCharacter ac)
    {
        ac.BeCure(0.03f*ac.maxHp, true, 0);

    }

    private void OnDestroy()
    {
        if (!hasAdd) return; 
        foreach (var _c in CharacterManager.instance.GetFriend(camp))
        {
            _c.OnEnergyFull -= Effect;
        }
    }
}
