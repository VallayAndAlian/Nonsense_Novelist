using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 动词：产卵
/// </summary>
class ChanLuan : AbstractVerbs
{
    static public string s_description = "使队友获得<color=#dd7d0e>虫卵</color>，持续10s";
    static public string s_wordName = "产卵";
    static public int rarity = 3;
    public override void Awake()
    {
        base.Awake();

        skillID = 14;
        wordName = "产卵";
        bookName = BookNameEnum.PHXTwist;
        description = "使队友获得<color=#dd7d0e>虫卵</color>，持续10s";

       // nickname.Add( "刺痛");

        skillMode = gameObject.AddComponent<CureMode>();
        skillMode.attackRange = new SingleSelector();
        skillEffectsTime = 10;

        rarity = 3;
        needCD=1;
    }
    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "ChongLuan";
        return _s;
    }
    public override void UseVerb(AbstractCharacter useCharacter)
    {
        base.UseVerb(useCharacter);
        BasicAbility(useCharacter);
    }

    public override void BasicAbility(AbstractCharacter useCharacter)
    {
     
        AbstractCharacter[] aim = skillMode.CalculateAgain(attackDistance, useCharacter);
        CollectionHelper.OrderByDescending(aim, p => Mathf.Abs(p.situation.number- useCharacter.situation.number));
        buffs.Add(aim[0].gameObject.AddComponent<ChongLuan>());
        buffs[0].maxTime = skillEffectsTime;  
    }

    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        if (character == null)
            return null;

        return character.wordName + "的心爱之人对其说：“闻君有两意，故来相决绝”，因而悲痛欲绝。";

    }

}
