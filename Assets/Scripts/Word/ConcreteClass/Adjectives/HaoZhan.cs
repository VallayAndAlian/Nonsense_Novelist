using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ݴʣ���ս��
/// </summary>
public class HaoZhan : AbstractAdjectives
{

    static public string s_description = "���<color=#dd7d0e>����</color>*2������10s";
    static public string s_wordName = "��ս��";
    static public int s_rarity = 1;
    public override void Awake()
    {                
        skillEffectsTime = 10;
       
        adjID = 20;
        wordName = "��ս��";
        bookName = BookNameEnum.PHXTwist;
        description = "���<color=#dd7d0e>����</color>*2������10s";

        skillMode = gameObject.AddComponent<SelfMode>();


        rarity = 1;
        base.Awake();
    }

    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "WordCollision";
        return _s;
    }



    bool hasOther = false;
    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);
        buffs.Add(aimCharacter.gameObject.AddComponent<HuoRe>());
        buffs[0].maxTime = skillEffectsTime;
        buffs.Add(aimCharacter.gameObject.AddComponent<HuoRe>());
        buffs[1].maxTime = skillEffectsTime;
        BasicAbility(aimCharacter);
    }

    float record;
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {
   
      
    
    }



    public override void End()
    {
      
        base.End(); Destroy(this);
       
    }

}
