using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨�����ܡ�����������
/// </summary>
public class NengLiangHuanChong : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.QiaoSi;
        settingName = "��������";
        res_name = "nenglianghuanchong";
        info = "��ɫÿӵ��1�����㣬�ܵ��������˺�-5%";
        lables = new List<string> { "����" };
        hasAdd = false;
        Init();
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

    float record=0;//��ʱ����attackAmountϵ��


    //ÿ�λ��һ���������ʱ�򣬶�ִ�д˺���
    void Effect(AbstractCharacter ac)
    {
        //Ч�������������ɫÿ����һ��������ʱ����ǰ���м��ܵ��������ܺͣ������˺�ϵ��
        int count=0;//���ڼ�¼��ǰ�����������
        foreach (var _skill in ac.skills)
        {
            count += _skill.CD;//��ȡ���м��ܵĵ�ǰ������
        }
        //�ָ�ϵ��
        ac.attackAmount += record;
        //����ϵ��
        ac.attackAmount -= count * 0.05f;
        //��¼ϵ���Ա��´λָ�
        record = count * 0.03f;
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
