using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 动词：葬花
/// </summary>
class BuryFlower : AbstractVerbs
{
    static public string s_description = "被动：普通攻击使对方获得<color=#dd7d0e>花瓣</color>;" +
        "\n主动：收回所有<color=#dd7d0e>花瓣</color>，并造成<color=#dd7d0e>花瓣</color>数 * 30 % <sprite name=\"psy\">的伤害";
    static public string s_wordName = "葬花";

    public override void Awake()
    {
        base.Awake();
        skillID = 2;
        wordName = "葬花";
        bookName = BookNameEnum.HongLouMeng;
        description = "被动：普通攻击使对方获得<color=#dd7d0e>花瓣</color>;\n主动：收回所有<color=#dd7d0e>花瓣</color>，并造成<color=#dd7d0e>花瓣</color>数 * 30 % <sprite name=\"psy\">的伤害";
        skillMode = gameObject.AddComponent<SelfMode>();
        skillEffectsTime = 10;
        rarity = 3;
        needCD = 6;
    }


    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "HuaBan";
        return _s;
    }



    /// <summary>
    /// 花瓣
    /// </summary>
    /// <param name="useCharacter">施法者</param>
    public override void UseVerb(AbstractCharacter useCharacter)
    {
        base.UseVerb(useCharacter);
       
        buffs.Add(skillMode.CalculateAgain(attackDistance, useCharacter)[0].gameObject.AddComponent<HuaBan>());
        buffs[0].maxTime = skillEffectsTime;


       
    }

    public override string UseText()
    {
        AbstractCharacter character = this.GetComponent<AbstractCharacter>();
        if (character == null)
            return null;

        return "林间盛开的桃花随轻风飘落在地。\n"+character.wordName+"将飘落在地的桃花聚拢成团，并将其埋葬，为其哀悼。“花谢花飞花满天，红香消断有谁怜？”";

    }
    
}
