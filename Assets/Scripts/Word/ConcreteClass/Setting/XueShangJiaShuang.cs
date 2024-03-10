using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨���쳣������ѩ�ϼ�˪
/// </summary>
public class XueShangJiaShuang : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.QiaoSi;
        settingName = "ѩ�ϼ�˪";
        info = "����״̬�ɵ��Ӹ����";
        lables = new List<string> { "�쳣"};
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
