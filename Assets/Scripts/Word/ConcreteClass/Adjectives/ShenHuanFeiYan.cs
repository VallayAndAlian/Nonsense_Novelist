using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �����׵�
/// </summary>
public class ShenHuanFeiYan : AbstractAdjectives
{
    static public string s_description = "<color=#dd7d0e>����</color>������20s";
    static public string s_wordName = "�����׵�";
    static public int rarity = 2;
    public override void Awake()
    {
        adjID = 15;
        wordName = "�����׵�";
        bookName = BookNameEnum.FluStudy;
        description = "<color=#dd7d0e>����</color>������20s";
        skillMode = gameObject.AddComponent<DamageMode>();

        skillEffectsTime = 20;
        rarity = 2;
        base.Awake();
        if (this.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
            wordCollisionShoots[0] = gameObject.AddComponent<ChuanBoCollision>();
    }

    override public string[] DetailLable()
    {
        string[] _s = new string[2];
        _s[0] = "ChuanBoCollision";
        _s[1] = "Ill";
        return _s;
    }
    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);
       

        buffs.Add(aimCharacter.gameObject.AddComponent<Ill>());
        buffs[0].maxTime = skillEffectsTime;
        //�����ɫ����ӣ������Ҳ����

    }
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {
    }

    

    public override void End()
    {
        base.End();
    }

}
