using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 名词：毒腺
/// </summary>
class DuXian : AbstractItems
{
    static public string s_description = "自身与随从的攻击附带<color=#dd7d0e>腐蚀</color>";
    static public string s_wordName = "毒腺";
    public override void Awake()
    {
        base.Awake();
        itemID = 19;
        wordName = "毒腺";
        bookName = BookNameEnum.CrystalEnergy;
        
        description = "自身与随从的攻击附带<color=#dd7d0e>腐蚀</color>";
        holdEnum = HoldEnum.handSingle;
        VoiceEnum = MaterialVoiceEnum.Ceram;

        rarity = 2;
    }

    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "FuShi";
        return _s;
    }
    bool hasAdd = false;
    public override void UseItem(AbstractCharacter chara)
    {
        base.UseItem(chara);
        if (hasAdd) return;
        //为自身和所有随从，增加平A附加效果
        var _acs = GetComponentsInChildren<AbstractCharacter>();
        foreach (var _ac in _acs)
        {
            _ac.event_AttackA += AddToAttackA;
        }
        hasAdd = true;
    }

    public override void UseVerb()
    {
        base.UseVerb();
       

    }
    public void AddToAttackA()
    {
        var _ac = GetComponent<AbstractCharacter>();
         //为攻击目标增加Buff
        var buff = _ac.myState.aim.gameObject.AddComponent<FuShi>();
        buffs.Add(buff);
        buff.maxTime = 7;
    }
    public override void End()
    {
        base.End();
        //为自身和所有随从，去掉平A附加效果
        var _acs = GetComponentsInChildren<AbstractCharacter>();
        foreach (var _ac in _acs)
        {
            _ac.event_AttackA -= AddToAttackA;
        }
    }
}
