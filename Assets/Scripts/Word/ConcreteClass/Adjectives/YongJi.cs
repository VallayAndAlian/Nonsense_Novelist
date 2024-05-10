using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ݴʣ�ӵ����
/// </summary>
public class YongJi : AbstractAdjectives
{
    static public string s_description = "���1�������ӣ������������<color=#dd7d0e>��ѣ</color>5s";
    static public string s_wordName = "ӵ����";
    static public int s_rarity = 1;
    public override void Awake()
    {
        adjID = 5;
        wordName = "ӵ����";
        bookName = BookNameEnum.ZooManual;
        description = "���1�������ӣ������������<color=#dd7d0e>��ѣ</color>5s";
        skillMode = gameObject.AddComponent<SelfMode>();
        skillEffectsTime = 5;
        rarity = 1; 

        base.Awake();

        if (this.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
            wordCollisionShoots[0] = gameObject.AddComponent<ChuanBoCollision>();

      
    }
    override public string[] DetailLable()
    {
        string[] _s = new string[1];
        _s[0] = "Dizzy";
        return _s;
    }
    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);
        BasicAbility(aimCharacter);
    }
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {
        if (aimCharacter.servants.Count >= 3)
        {
            buffs.Add(aimCharacter.gameObject.AddComponent<Dizzy>());
            buffs[0].maxTime = skillEffectsTime;
            return;
        }
        
        this.GetComponent<AbstractCharacter>().AddRandomServant();
    }

    

    public override void End()
    {
        base.End();
       
    }

}
