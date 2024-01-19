using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///名词: Nexus-6型手臂
/// </summary>
class Nexus_6Arm : AbstractItems
{
    static public string s_description = "<sprite name=\"atk\">+5，获得<color=#dd7d0e>改造</color>*3";
    static public string s_wordName = "Nexus-6型手臂";
    public override void Awake()
    {
        base.Awake();
        itemID = 13;
        wordName = "Nexus-6型手臂";
        bookName = BookNameEnum.ElectronicGoal;
        description = "<sprite name=\"atk\">+5，获得<color=#dd7d0e>改造</color>*3";
        VoiceEnum = MaterialVoiceEnum.Meat;

        rarity = 3;
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
        chara.atk += 5;
        buffs.Add(gameObject.AddComponent<GaiZao>());
        buffs[0].maxTime = Mathf.Infinity;
    }

    public override void UseVerb()
    {
        base.UseVerb();
    }

    public override void End()
    {
        base.End();
        aim.atk -= 5;
    }
}
