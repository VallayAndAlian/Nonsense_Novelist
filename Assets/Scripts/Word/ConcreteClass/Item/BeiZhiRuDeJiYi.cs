using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 名词：被植入的记忆
/// </summary>
class BeiZhiRuDeJiYi : AbstractItems
{
    static public string s_description = "<sprite name=\"psy\">-15%，<sprite name=\"san\">-15%，获得<color=#dd7d0e>改造</color>";
    static public string s_wordName = "被植入的记忆";
    public override void Awake()
    {
        base.Awake();
        itemID = 14;
        wordName = "被植入的记忆";
        bookName = BookNameEnum.ElectronicGoal;
        description = "<sprite name=\"psy\">-15%，<sprite name=\"san\">-15%，获得<color=#dd7d0e>改造</color>";
        VoiceEnum = MaterialVoiceEnum.Meat;

        rarity = 1;
    }

    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "GaiZao";
        return _s;
    }

    public override void UseItem(AbstractCharacter chara)
    {
        base.UseItem(chara);
        chara.psyMul -= 0.15f;
        chara.sanMul -= 0.15f;
        buffs.Add(chara.gameObject.AddComponent<GaiZao>());
        buffs[0].maxTime = Mathf.Infinity;

    }

    public override void UseVerb()
    {
        base.UseVerb();
    }

    public override void End()
    {
        base.End();
        aim.psyMul += 0.15f;
        aim.sanMul += 0.15f;
    }
}
