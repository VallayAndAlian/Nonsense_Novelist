using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ݴʣ��۲Ƶ�
/// </summary>

public class JuCaiDe : AbstractAdjectives
{
    static public string s_description = "ʹ��ɫ��õ���һ�����ʷ���";
    static public string s_wordName = "�۲Ƶ�";
    static public int rarity = 2;


    public override void Awake()
    {

        adjID = 29;
        wordName = "�۲Ƶ�";
        bookName = BookNameEnum.CrystalEnergy;
        description = "ʹ��ɫ��õ���һ�����ʷ���";


        skillEffectsTime = Mathf.Infinity;

        rarity = 2;
        base.Awake();

        if (this.gameObject.layer == LayerMask.NameToLayer("WordCollision"))
            wordCollisionShoots[0] = gameObject.AddComponent<ChuanBoCollision>();
    }




    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);

        buffs.Add(aimCharacter.gameObject.AddComponent<JuCai>());
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
