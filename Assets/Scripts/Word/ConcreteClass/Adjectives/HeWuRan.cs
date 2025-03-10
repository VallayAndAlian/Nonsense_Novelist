using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ݴʣ�����Ⱦ��
/// </summary>
public class HeWuRan : AbstractAdjectives
{
    static public string s_description = "<color=#dd7d0e>�ж�</color>������10s";
    static public string s_wordName = "����Ⱦ��";
    static public int s_rarity = 2;
    public override void Awake()
    {  
        
        adjID = 13;
        wordName = "����Ⱦ��";
        bookName = BookNameEnum.ElectronicGoal;
        description = "<color=#dd7d0e>�ж�</color>������10s";

        skillMode = gameObject.AddComponent<DamageMode>();

        skillEffectsTime = 10;
        rarity = 2;

        base.Awake();

        if (this.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
            wordCollisionShoots[0] = gameObject.AddComponent<ChuanBoCollision>();
    }


    override public string[] DetailLable()
    {
        string[] _s = new string[2];
        _s[0] = "ChuanBoCollision";
        _s[1] = "Toxic";
        return _s;
    }

    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);
        buffs.Add(aimCharacter.gameObject.AddComponent<Toxic>());
        buffs[0].maxTime = skillEffectsTime;
    }
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {
    }

    

    public override void End()
    {
        base.End();
    }

}
