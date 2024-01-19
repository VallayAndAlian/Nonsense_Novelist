using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 服药
/// </summary>
class FuYao_x : AbstractVerbs
{
    public override void Awake()
    {
        base.Awake();
        skillID = 5;
        wordName = "服药";
        bookName = BookNameEnum.HongLouMeng;
        description = "学会服药，恢复20点生命，解除负面效果。";
        skillMode = gameObject.AddComponent<CureMode>();
        skillMode.attackRange = new SingleSelector();
        skillEffectsTime = 0;
        needCD = 6;
        description = "";
    }
    /// <summary>
    /// 回满血
    /// </summary>
    /// <param name="useCharacter">施法者</param>
    public override void UseVerb(AbstractCharacter useCharacter)
    {
        useCharacter.teXiao.PlayTeXiao("LengXiangWan");
        base.UseVerb(useCharacter);
        /*foreach (AbstractCharacter aim in aims)
        {
            aim.hp = aim.maxHP;
        }*/
        BasicAbility(useCharacter);
    }
    /// <summary>
    /// 解除所有负面状态
    /// </summary>
    public override void BasicAbility(AbstractCharacter useCharacter)
    {
        useCharacter.dizzyTime = 0;
    }

    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        if (character == null)
            return null;

        return character.wordName + "曾从一海上来的僧人学得一仙方，给名字2服下了这仙方，名为冷香丸。";

    }
}
