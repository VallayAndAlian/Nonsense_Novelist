using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 形容词：不朽
/// </summary>

public class BuXiu : AbstractAdjectives
{
    static public string s_description = "<sprite name=\"hpmax\">+60，获得<color=#dd7d0e>复活</color>";
    static public string s_wordName = "不朽的";


    public override void Awake()
    {
        
        adjID = 5;
        wordName = "不朽的";
        bookName = BookNameEnum.EgyptMyth;
        description = "<sprite name=\"hpmax\">+60，获得<color=#dd7d0e>复活</color>";

        skillMode = gameObject.AddComponent<CureMode>();
        skillEffectsTime = Mathf.Infinity;

        rarity = 2;
        base.Awake();
    }

    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "ReLife";
        return _s;
    }


    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);

        buffs.Add(aimCharacter.gameObject.AddComponent<ReLife>());
        buffs[0].maxTime = skillEffectsTime;

        BasicAbility(aimCharacter);
    }
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {
        aimCharacter.maxHp += 60;
        aim.CreateFloatWord(60, FloatWordColor.healMax, false);

    }

    

    public override void End()
    {
        base.End();
        aim.maxHp -= 60;
    }

}
