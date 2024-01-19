using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 名词：玻璃挂坠
/// </summary>
class BoLiGuaZhui : AbstractItems
{
    static public string s_description = "<sprite name=\"san\">+3";
    static public string s_wordName = "玻璃挂坠";
    public override void Awake()
    {
        base.Awake();
        itemID = 23;
        wordName = "玻璃挂坠";
        bookName = BookNameEnum.allBooks;
        description = "<sprite name=\"san\">+3";

        VoiceEnum = MaterialVoiceEnum.Ceram;

        rarity = 0;
    }

    public override void UseItem(AbstractCharacter chara)
    {
        base.UseItem(chara);
        chara.san += 3;
    }

    public override void UseVerb()
    {
        base.UseVerb();
    }

    public override void End()
    {
        base.End();
        aim.san -= 3;
    }
}
