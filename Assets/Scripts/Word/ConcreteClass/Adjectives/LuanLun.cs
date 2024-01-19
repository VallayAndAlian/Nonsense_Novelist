using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ݴʣ����׵�
/// </summary>
public class LuanLun : AbstractAdjectives
{
    static public string s_description = "<color=#dd7d0e>����</color>һȺ��ɫ����������7s";
    static public string s_wordName = "���׵�";
    public override void Awake()
    {
        base.Awake();
        adjID = 8;
        wordName = "���׵�";
        bookName = BookNameEnum.Salome;
        description = "<color=#dd7d0e>����</color>һȺ��ɫ����������7s";
        skillMode = gameObject.AddComponent<DamageMode>();
        skillEffectsTime =7;
        rarity = 1;

        if (this.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
            wordCollisionShoots[0] = gameObject.AddComponent<ChuanBoCollision>();

    }
    override public string[] DetailLable()
    {
        string[] _s = new string[2];
        _s[0] = "ChuanBoCollision";
        _s[1] = "FuHuo";
        return _s;
    }


    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);
        buffs.Add(aimCharacter.gameObject.AddComponent<FuHuo>());
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
