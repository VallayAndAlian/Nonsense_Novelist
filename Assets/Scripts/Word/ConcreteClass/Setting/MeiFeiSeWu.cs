using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨�����ܡ���ü��ɫ��
/// </summary>
public class MeiFeiSeWu : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.GuiCai;
        settingName = "ü��ɫ��";
        res_name = "meifeisewu";
        info = "�����ܡ��ṩ������+1";
        lables = new List<string> { "����"};
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
    void Effect(AbstractCharacter ac)
    {
        var _kf= ac.GetComponents<KangFen>();
        foreach(var it in _kf)
        {
            it.nl += 1;
        }

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
