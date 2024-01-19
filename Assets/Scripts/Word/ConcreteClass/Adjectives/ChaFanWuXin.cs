using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 形容词：茶饭无心的
/// </summary>
public class ChaFanWuXin : AbstractAdjectives
{
    static public string s_description = "在10s内 <sprite name=\"psy\">+8，无法攻击";
    static public string s_wordName = "茶饭无心的";


    public override void Awake()
    {
        
        adjID = 1;
        wordName = "茶饭无心的";
        bookName = BookNameEnum.HongLouMeng;
        description = "在10s内 <sprite name=\"psy\">+8，无法攻击";

        skillMode = gameObject.AddComponent<UpPSYMode>();
        skillEffectsTime = 10;

        rarity = 1;
        base.Awake();
    }

    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);

        buffs.Add(aimCharacter.gameObject.AddComponent<Upset>());
        buffs[0].maxTime = skillEffectsTime;

        BasicAbility(aimCharacter);
    }
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {
        aimCharacter.psy += 8;
    }

    public override void End()
    {
        base.End();
        aim.psy -= 8;
    }
}
