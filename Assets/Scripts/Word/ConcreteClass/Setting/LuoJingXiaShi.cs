using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨����ɫ�����侮��ʯ
/// </summary>
public class LuoJingXiaShi : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.PingYong;
        settingName = "�侮��ʯ";
        info = "ʧ���Ի�þ�ɥ�ĵ��ˣ�׷��һ�Ρ����顱";
        lables = new List<string> { "��ɫ","��Ƶ"};
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
