using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 形容词：快速成长的
/// </summary>
public class QuicklyGrowing : AbstractAdjectives
{
    static public string s_description = "<sprite name=\"hp\">恢复30";
    static public string s_wordName = "快速成长的";
    public override void Awake()
    {
        adjID = 20;
        wordName = "快速成长的";
        bookName = BookNameEnum.allBooks;
        description = "<sprite name=\"hp\">恢复30";
        skillMode = gameObject.AddComponent<SelfMode>();
        skillEffectsTime = Mathf.Infinity;
        rarity = 0;
        time = skillEffectsTime;
        base.Awake();
    }

    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);
    }
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {
        aimCharacter.hp += 30;
        aimCharacter.CreateFloatWord(30, FloatWordColor.heal, false);
    }

    float time;
    protected override void Update()
    {
        base.Update();
       
    }

    public override void End()
    {
        //aim.hp -= 30;
        base.End();
    }

}
