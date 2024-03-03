using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 形容词：拥挤的
/// </summary>
public class YongJi : AbstractAdjectives
{
    static public string s_description = "获得1个随机随从";
    static public string s_wordName = "拥挤的";
    static public int rarity = 1;
    public override void Awake()
    {
        adjID = 5;
        wordName = "拥挤的";
        bookName = BookNameEnum.ZooManual;
        description = "获得1个随机随从";
        skillMode = gameObject.AddComponent<SelfMode>();
        skillEffectsTime = 1;
        rarity = 1; 

        base.Awake();

        if (this.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
            wordCollisionShoots[0] = gameObject.AddComponent<ChuanBoCollision>();

      
    }

    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);
        BasicAbility(aimCharacter);
    }
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {
        this.GetComponent<AbstractCharacter>().AddRandomServant();
    }

    

    public override void End()
    {
        base.End();
       
    }

}
