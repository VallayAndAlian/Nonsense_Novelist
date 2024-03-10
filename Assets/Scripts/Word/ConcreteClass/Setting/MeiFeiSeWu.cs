using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【大技能】：眉飞色舞
/// </summary>
public class MeiFeiSeWu : AbstractSetting
{
    AbstractCharacter chara;

    public override void Awake()
    {
        base.Awake();
        level = SettingLevel.GuiCai;
        settingName = "眉飞色舞";
        info = "“亢奋”提供的能量+1";
        lables = new List<string> { "蓄能"};
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
