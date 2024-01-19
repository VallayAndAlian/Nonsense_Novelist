using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 名词：荷鲁斯之眼
/// </summary>
class herusizhiyan : AbstractItems
{
    static public string s_description = "<sprite name=\"atk\">+4";
    static public string s_wordName = "荷鲁斯之眼";
    public override void Awake()
    {
        base.Awake();

        itemID = 7;

        wordName = "荷鲁斯之眼";
        bookName = BookNameEnum.EgyptMyth;
        description = "<sprite name=\"atk\">+4";
        VoiceEnum = MaterialVoiceEnum.Ceram;
        rarity = 2;
    }
    public override void UseItem(AbstractCharacter chara)
    {
        base.UseItem(chara);
        chara.atk += 4;
    }

    public override void UseVerb()
    {
        base.UseVerb();
        
    }

    public override void End()
    {
        base.End();
        aim.atk -= 4;
    }
}
