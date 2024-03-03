using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 形容词：好战的
/// </summary>
public class HaoZhan : AbstractAdjectives
{

    static public string s_description = "快速攻击，持续5s";
    static public string s_wordName = "好战的";
    static public int rarity = 1;
    public override void Awake()
    {                
        skillEffectsTime = 5;
       
        adjID = 20;
        wordName = "好战的";
        bookName = BookNameEnum.PHXTwist;
        description = "快速攻击，持续5s";

        skillMode = gameObject.AddComponent<SelfMode>();


        rarity = 1;
        base.Awake();
    }
    bool hasOther = false;
    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);
     
        BasicAbility(aimCharacter);
    }

    float record;
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {
   
        record = aimCharacter.attackSpeedPlus;
        aimCharacter.attackSpeedPlus += 0.5f;
    
    }

    protected override void Update()
    {
      
        base.Update();
    }

    public override void End()
    {
      
        base.End();  aim.attackSpeedPlus -= 0.5f; Destroy(this);
       
    }

}
