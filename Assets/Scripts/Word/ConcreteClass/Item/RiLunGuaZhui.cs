using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 日轮挂坠
/// </summary>
class RiLunGuaZhui : AbstractItems
{
    static public string s_description = "<sprite name=\"hpmax\">+30，恢复+3";
    static public string s_wordName = "日轮挂坠";
    public override void Awake()
    {
        base.Awake();
        itemID = 6;

        wordName = "日轮挂坠";
        bookName = BookNameEnum.EgyptMyth;
        description = "<sprite name=\"hpmax\">+30，恢复+3";
        VoiceEnum = MaterialVoiceEnum.Ceram;
        rarity = 2;
    }
    public override void UseItem(AbstractCharacter chara)
    {
        base.UseItem(chara);
        //生命上限+30，恢复+3
        chara.cure += 3;
        chara.maxHp += 30;
        chara.CreateFloatWord(30, FloatWordColor.healMax, false);

    }

    public override void UseVerb()
    {
        base.UseVerb();
        
    }

    public override void End()
    {
        base.End();
        aim.maxHp -= 5;
        aim.cure -= 3;
    }
}
