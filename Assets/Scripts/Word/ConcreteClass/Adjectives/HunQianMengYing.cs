using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ݴʣ���ǣ���ӵ�
/// </summary>
public class HunQianMengYing : AbstractAdjectives
{
    static public string s_description = "<color=#dd7d0e>���ң�����</color>������10s";
    static public string s_wordName = "��ǣ���ӵ�";
    static public int s_rarity = 1;

    public override void Awake()
    {
        adjID = 11;
        wordName = "��ǣ���ӵ�";
        bookName = BookNameEnum.Salome;
        description = "<color=#dd7d0e>���ң�����</color>������10s";

        skillMode = gameObject.AddComponent<DamageMode>();

        skillEffectsTime = 10;
        rarity = 1;
        base.Awake();
    }
    override public string[] DetailLable()
    {
        string[] _s = new string[2];
        _s[0] = "YiLuan";
        _s[1] = "XuRuo";
        return _s;
    }

    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);

      
        buffs.Add(aimCharacter.gameObject.AddComponent<YiLuan>());
        buffs[0].maxTime = skillEffectsTime;
        buffs.Add(aimCharacter.gameObject.AddComponent<XuRuo>());
        buffs[1].maxTime = skillEffectsTime;
    }
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {
    }

    

    public override void End()
    {
        base.End();
    }

}
