using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 动词：图灵测试
/// </summary>
class TuLingCeShi : AbstractVerbs
{
    static public string s_description = "被动：对 <sprite name=\"san\">低于10的敌人造成30%额外伤害;\n主动：使敌人受到自身 <sprite name=\"atk\"> +  <sprite name=\"san\">的精神伤害";
    static public string s_wordName = "图灵测试";


    public override void Awake()
    {
        base.Awake();
        skillID = 9;
        wordName = "图灵测试";
        bookName = BookNameEnum.ElectronicGoal;
        description = "被动：对 <sprite name=\"san\">低于10的敌人造成30%额外伤害;\n主动：使敌人受到自身 <sprite name=\"atk\"> +  <sprite name=\"san\">的精神伤害";

        skillMode = gameObject.AddComponent<DamageMode>();
        (skillMode as DamageMode).isPhysics = false;
        skillMode.attackRange =  new SingleSelector();
        skillEffectsTime = Mathf.Infinity;

        rarity = 3;
        needCD = 3;
    

    }

    public override void UseVerb(AbstractCharacter useCharacter)
    {
        base.UseVerb(useCharacter);
        BasicAbility(useCharacter);
    }

    public override void BasicAbility(AbstractCharacter useCharacter)
    {
        AbstractCharacter aim = skillMode.CalculateAgain(attackDistance, useCharacter)[0];
        aim.CreateFloatWord(
        skillMode.UseMode(useCharacter, (aim.atk - aim.psy) * 10 * (1 - aim.san / (aim.san + 20)), aim)
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
