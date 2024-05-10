using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ݴʣ���������
/// </summary>

public class FuNengLiangDe : AbstractAdjectives
{
    static public string s_description = "<color=#dd7d0e>ľګ�����ң�����</color>������10s";
    static public string s_wordName = "��������";
    static public int s_rarity = 2;


    public override void Awake()
    {

        adjID = 30;
        wordName = "��������";
        bookName = BookNameEnum.CrystalEnergy;
        description ="<color=#dd7d0e>ľګ�����ң�����</color>������10s";


        skillEffectsTime = 10;

        rarity = 2;
        base.Awake();
    }

    override public string[] DetailLable()
    {
        string[] _s = new string[4];
        _s[0] = "MuNe";
        _s[1] = "YiLuan";
        _s[2] = "HanLen";

        return _s;
    }


    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);

        buffs.Add(aimCharacter.gameObject.AddComponent<MuNe>());
        buffs[0].maxTime = skillEffectsTime;
        buffs.Add(aimCharacter.gameObject.AddComponent<YiLuan>());
        buffs[1].maxTime = skillEffectsTime;
        buffs.Add(aimCharacter.gameObject.AddComponent<HanLen>());
        buffs[2].maxTime = skillEffectsTime;
    }
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {


    }



    public override void End()
    {
        base.End();

    }

}
