using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 动词：免疫增强
/// </summary>
class MianYiZengQiang : AbstractVerbs
{
    static public string s_description = "<sprite name=\"hpmax\">+40，并消除负面状态”";
    static public string s_wordName = "免疫增强";
    public override void Awake()
    {
        base.Awake();
        skillID = 12;
        wordName = "免疫增强";
        bookName = BookNameEnum.FluStudy;
        description = "<sprite name=\"hpmax\">+40，并消除负面状态”";

        skillMode = gameObject.AddComponent<SelfMode>();
        skillMode.attackRange =  new SingleSelector();
        skillEffectsTime = Mathf.Infinity;

        rarity = 2;
        needCD =4;

    }
    /// <summary>
    /// 复活
    /// </summary>
    /// <param name="useCharacter">施法者</param>
    public override void UseVerb(AbstractCharacter useCharacter)
    {

        base.UseVerb(useCharacter);
        BasicAbility(useCharacter);
    }

    public override void BasicAbility(AbstractCharacter useCharacter)
    {
        AbstractCharacter aim= skillMode.CalculateAgain(attackDistance,useCharacter)[0];
        aim.CreateFloatWord(
        skillMode.UseMode(useCharacter, 40, aim)
        ,FloatWordColor.heal,true);
        aim.maxHp += 40;
        aim.CreateFloatWord(40, FloatWordColor.healMax, false);

        var _buffs = character.GetComponents<AbstractBuff>();
        foreach (var _buff in _buffs)
        {
            if (_buff.isBad) Destroy(_buff);
        }
    }

    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        //if (character == null || aimState==null)
            //return null;

        return character.wordName + "拿起小刀，将腹部开出一个小缺口，并将香脂油灌满其中。再将树脂填入名字2的头颅，防止头部的变形。接下来将他整个埋于碱粉中一个月，这样就可以做到肉体不被腐朽所困扰了。";

    }
}
