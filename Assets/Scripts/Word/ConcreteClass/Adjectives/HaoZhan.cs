using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ݴʣ���ս��
/// </summary>
public class HaoZhan : AbstractAdjectives
{

    static public string s_description = "���ٹ���������5s";
    static public string s_wordName = "��ս��";
    static public int rarity = 1;
    public override void Awake()
    {                
        skillEffectsTime = 5;
       
        adjID = 20;
        wordName = "��ս��";
        bookName = BookNameEnum.PHXTwist;
        description = "���ٹ���������5s";

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
