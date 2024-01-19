using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 动词：防腐
/// </summary>
class FangFuShu : AbstractVerbs
{
    static public string s_description = "使友方<color=#dd7d0e>复活</color>，持续10s";
    static public string s_wordName = "防腐";
    public override void Awake()
    {
        base.Awake();
        skillID = 5;
        wordName = "防腐";
        bookName = BookNameEnum.EgyptMyth;
        description = "使友方<color=#dd7d0e>复活</color>，持续10s";

        //目标：血量最低的友方
        skillMode = gameObject.AddComponent<CureMode>();
        skillMode.attackRange =  new SingleSelector();


        skillEffectsTime = 10;
        rarity = 1;
        needCD = 6;


    }

    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "ReLife";
        return _s;
    }

    /// <summary>
    /// 复活
    /// </summary>
    /// <param name="useCharacter">施法者</param>
    public override void UseVerb(AbstractCharacter useCharacter)
    {
        base.UseVerb(useCharacter);
        buffs.Add(skillMode.CalculateAgain(attackDistance, useCharacter)[0].gameObject.AddComponent<ReLife>());
        buffs[0].maxTime = skillEffectsTime;
    }

    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        //if (character == null || aimState==null)
            //return null;

        return character.wordName + "拿起小刀，将腹部开出一个小缺口，并将香脂油灌满其中。再将树脂填入名字2的头颅，防止头部的变形。接下来将他整个埋于碱粉中一个月，这样就可以做到肉体不被腐朽所困扰了。";

    }
}
