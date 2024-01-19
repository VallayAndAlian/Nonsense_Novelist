using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 动词：亲吻
/// </summary>
class Kiss : AbstractVerbs
{
    static public string s_description = "使敌人受到3*<sprite name=\"psy\">的精神伤害，并被<color=#dd7d0e>俘获</color>10s";
    static public string s_wordName = "亲吻";
    public override void Awake()
    {
        base.Awake();
        skillID = 7;
        wordName = "亲吻";
        bookName = BookNameEnum.Salome;
        description = "使敌人受到3*<sprite name=\"psy\">的精神伤害，并被<color=#dd7d0e>俘获</color>10s";

        skillMode = gameObject.AddComponent<DamageMode>();
        (skillMode as DamageMode).isPhysics = false;
        skillMode.attackRange =  new SingleSelector();

        skillEffectsTime =5;
        rarity = 1;
        needCD = 4;

    }
    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "FuHuo";
        return _s;
    }
    public override void UseVerb(AbstractCharacter useCharacter)
    {
        base.UseVerb(useCharacter);
        buffs.Add(skillMode.CalculateAgain(attackDistance, useCharacter)[0].gameObject.AddComponent<FuHuo>());
        buffs[0].maxTime = 10f;
        BasicAbility(useCharacter);
    }

    public override void BasicAbility(AbstractCharacter useCharacter)
    {
        AbstractCharacter aim = skillMode.CalculateAgain(attackDistance, useCharacter)[0];
        aim.CreateFloatWord(
        skillMode.UseMode(useCharacter, 3 * useCharacter.psy * useCharacter.psyMul * (1 - aim.san / (aim.san + 20)), aim)
        ,FloatWordColor.psychic,true);
    }
    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        //if (character == null || aimState==null)
            //return null;

        return character.wordName + "拿起小刀，将腹部开出一个小缺口，并将香脂油灌满其中。再将树脂填入名字2的头颅，防止头部的变形。接下来将他整个埋于碱粉中一个月，这样就可以做到肉体不被腐朽所困扰了。";

    }
}
