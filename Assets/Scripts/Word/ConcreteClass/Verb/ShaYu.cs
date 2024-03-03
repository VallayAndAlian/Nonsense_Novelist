using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 动词：沙浴
/// </summary>
class ShaYu : AbstractVerbs
{
    static public string s_description = "治疗10+100%<sprite name=\"san\">，消除3层减益状态";
    static public string s_wordName = "沙浴";
    static public int rarity = 2;
    public override void Awake()
    {
        base.Awake();
        skillID = 4;
        wordName = "沙浴";
        bookName = BookNameEnum.ZooManual;
        description = "治疗10+100%<sprite name=\"san\">，消除3层减益状态";

        skillMode = gameObject.AddComponent<SelfMode>();

        skillEffectsTime = Mathf.Infinity;
        rarity = 2;
        needCD = 4;
    }



    /// <summary>
    /// 亢奋
    /// </summary>
    /// <param name="useCharacter">施法者</param>
    public override void UseVerb(AbstractCharacter useCharacter)
    {
        base.UseVerb(useCharacter);
        BasicAbility(useCharacter);
     
    }

    List<AbstractBuff> badBuff=new List<AbstractBuff>();
    public override void BasicAbility(AbstractCharacter useCharacter)
    {  
        //治疗10+100%意志
        AbstractCharacter aim = skillMode.CalculateAgain(attackDistance, useCharacter)[0];
        //aim.CreateFloatWord(
        //skillMode.UseMode(useCharacter, 10+ useCharacter.san*useCharacter.sanMul*1, aim)
        //, FloatWordColor.heal, true);
        skillMode.UseMode(AttackType.heal, 10 + useCharacter.san * useCharacter.sanMul * 1, useCharacter, aim, true, 0);


        badBuff.Clear();
        var _buffs = aim.GetComponents<AbstractBuff>();
        
        if (_buffs.Length == 0) return;

        int mostCount = 0;
        AbstractBuff mostBuff= _buffs[0];
        //寻找身上最多的负面组件
        foreach (var _buff in _buffs)
        {
            if (!badBuff.Contains(_buff))
            {
                if (_buff.isBad)
                {
                     badBuff.Add(_buff);
                    if(aim.GetComponents(_buff.GetType()).Length>mostCount)
                    {
                        mostBuff = _buff;
                        mostCount = aim.GetComponents(_buff.GetType()).Length;
                    }
                }
            }
        }
       if (mostCount == 0) return;
        int count = 0;
        foreach (var _buff in aim.GetComponents(mostBuff.GetType()))
        {
            Destroy(_buff);
            count++;
            if (count == 2) return;
        }

    }

    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        if (character == null)
            return null;

        return "林间盛开的桃花随轻风飘落在地。\n"+character.wordName+"将飘落在地的桃花聚拢成团，并将其埋葬，为其哀悼。“花谢花飞花满天，红香消断有谁怜？”";

    }
    
}
