using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///名词：奇怪石像
/// </summary>
class QiGuaiShiXiang : AbstractItems
{
    static public string s_description = " <sprite name=\"psy\">+1";
    static public string s_wordName = "奇怪石像";
    public override void Awake()
    {
        base.Awake();
        itemID = 22;
        wordName = "奇怪石像";
        bookName = BookNameEnum.allBooks;
        description = "<sprite name=\"psy\">+1";

        VoiceEnum = MaterialVoiceEnum.Ceram;

        rarity = 0;
    }

    public override void UseItem(AbstractCharacter chara)
    {
        base.UseItem(chara);
        chara.psy += 1;
    }

    public override void UseVerb()
    {
        base.UseVerb();
    }

    public override void End()
    {
        base.End();
        aim.psy -= 1;
    }
}
