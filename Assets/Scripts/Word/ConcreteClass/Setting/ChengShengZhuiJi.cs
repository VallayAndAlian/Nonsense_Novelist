using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �趨����Ƶ���ܡ�����ʤ׷��
/// </summary>
public class ChengShengZhuiJi : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.GuiCai;
        settingName = "ü��ɫ��";
        info = "��ɫÿ�ͷ�4�����ʣ��´ζ���׷��һ����ͬ����";
        lables = new List<string> { "��Ƶ"};
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
