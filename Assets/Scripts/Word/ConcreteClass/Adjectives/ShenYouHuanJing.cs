using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ݴʣ����λ�����
/// </summary>
public class ShenYouHuanJing : AbstractAdjectives
{
    static public string s_description = " <sprite name=\"atk\">�� <sprite name=\"psy\">����10s";
    static public string s_wordName = "���λþ���";
    public override void Awake()
    {
        adjID = 2;
        wordName = "���λþ���";
        bookName = BookNameEnum.HongLouMeng;
        description = " <sprite name=\"atk\">�� <sprite name=\"psy\">����10s";
        skillMode = gameObject.AddComponent<SelfMode>();
        skillEffectsTime = 10;
        rarity = 1;
        
        base.Awake();
        if (this.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
        {
            wordCollisionShoots[0] = gameObject.AddComponent<XuWu_YunSu>();
        }
    }

    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "XuWu_YunSu";
        return _s;
    }

    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);
    
        buffs.Add(aimCharacter.gameObject.AddComponent<DianDao>());
        buffs[0].maxTime = skillEffectsTime;
    }
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {
        
    }
    
}
