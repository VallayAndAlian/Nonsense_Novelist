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
        info = "�����ܡ��ṩ������+1";
        lables = new List<string> { "����"};
        hasAdd = false;
        Init();
    }
    public override void Init()
    {
        if (hasAdd) return;

        chara = CharacterManager.instance.gameObject.GetComponentInChildren<ShiLian>();
        if (chara != null)
        {
            chara.event_AddBuff += Effect;
        }
        hasAdd = true;
    }
    void Effect(AbstractBuff buff)
    {


    }
}
