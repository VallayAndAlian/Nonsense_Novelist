using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ݴʣ����������
/// </summary>


public class NanYiXiaoMieDe : AbstractAdjectives
{
    static public string s_description = "<color=#dd7d0e>�������</color>������10s";
    static public string s_wordName = "���������";
    static public int s_rarity = 3;


    public override void Awake()
    {

        adjID = 32;
        wordName = "���������";
        bookName = BookNameEnum.FluStudy;
        description = "<color=#dd7d0e>�������</color>������10s";


        skillEffectsTime = 10;

        rarity = 3;
        base.Awake();
    }

    override public string[] DetailLable()
    {
        string[] _s = new string[2];
        _s[0] = "ReLife";
        _s[1] = "ZaiSheng";
        return _s;
    }


    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);

        buffs.Add(aimCharacter.gameObject.AddComponent<ReLife>());
        buffs[0].maxTime = skillEffectsTime;
        buffs.Add(aimCharacter.gameObject.AddComponent<ZaiSheng>());
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
