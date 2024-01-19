using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 动词：心碎
/// </summary>
class HeartBroken : AbstractVerbs
{

    static public string s_description = "造成2*<sprite name=\"psy\">的精神伤害";
    static public string s_wordName = "心碎";
    public override void Awake()
    {
        base.Awake();
        skillID = 14;
        wordName = "心碎";
        bookName = BookNameEnum.allBooks;
        description = "造成2*<sprite name=\"psy\">的精神伤害";
        //nickname.Add( "刺痛");

        skillMode = gameObject.AddComponent<SelfMode>();
        skillMode.attackRange = new SingleSelector();
        skillEffectsTime = Mathf.Infinity;

        rarity = 1;
        needCD=2;
    }

    public override void UseVerb(AbstractCharacter useCharacter)
    {
       
        base.UseVerb(useCharacter);
        BasicAbility(useCharacter);
    }
    public override void BasicAbility(AbstractCharacter useCharacter)
    {
        //造成200%的精神伤害
        AbstractCharacter aim = skillMode.CalculateAgain(attackDistance, useCharacter)[0];
        aim.CreateFloatWord(
        skillMode.UseMode(useCharacter, 2 * useCharacter.atk * useCharacter.atkMul * (1 - aim.def / (aim.def + 20)), aim)
        , FloatWordColor.physics, true);
    }

    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        if (character == null)
            return null;

        return character.wordName + "的心爱之人对其说：“闻君有两意，故来相决绝”，因而悲痛欲绝。";

    }

}
