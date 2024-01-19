using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///（废弃） 动词：乙酸喷射
/// </summary>
class CHOOHShoot_x : AbstractVerbs
{
    public override void Awake()
    {
        base.Awake();
        skillID = 13;
        wordName = "蚁酸喷射";
        bookName = BookNameEnum.PHXTwist;
        description = "使敌人受到伤害，获得“腐蚀”";
        skillMode = gameObject.AddComponent<DamageMode>();
        (skillMode as DamageMode).isPhysics= true;
        skillMode.attackRange = new SingleSelector();
        skillEffectsTime = 12;
        rarity = 1;
        needCD = 1;

    }


    public override void UseVerb(AbstractCharacter useCharacter)
    {
        base.UseVerb(useCharacter);
        buffs.Add(skillMode.CalculateAgain(attackDistance, useCharacter)[0].gameObject.AddComponent<FuShi>());
        buffs[0].maxTime = skillEffectsTime;
        BasicAbility(useCharacter);
    }

    public override void BasicAbility(AbstractCharacter useCharacter)
    {
        AbstractCharacter aim = skillMode.CalculateAgain(attackDistance, useCharacter)[0];
        aim.CreateFloatWord(
        skillMode.UseMode(useCharacter, useCharacter.atk*0.25f * (1 - aim.def / (aim.def + 20)), aim)
        ,FloatWordColor.physics,true);
    }

    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        if (character == null)
            return null;

        return character.wordName + "鼓动着自己腹部的腺体，突然收缩腹部，喷射出了一道酸性的液体，正好命中了名字2的脸部。";

    }
}
