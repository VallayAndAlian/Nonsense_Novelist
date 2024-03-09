using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 设定【大技能】：超频冲击
/// </summary>
public class ChaoPinChongJi : AbstractSetting
{
    AbstractCharacter chara;

    public override void Start()
    {
        base.Start();
        level = SettingLevel.PingYong;
        name = "超频冲击";
        info = "角色每拥有1能量点，造成的最终伤害+3%";
        lables = new List<string> { "蓄能" };
        hasAdd = false;
        Init();
    }
    public override void Init()
    {
        if (hasAdd) return;

        chara = CharacterManager.instance.gameObject.GetComponentInChildren<SiYangYuan>();
        if (chara != null)
        {
            chara.event_AddBuff += Effect;
        }
        hasAdd = true;
    }
    void Effect(AbstractBuff buff)
    {


    }

    private void OnDestroy()
    {
        if (hasAdd)
            chara.event_AddBuff -= Effect;
    }
}
