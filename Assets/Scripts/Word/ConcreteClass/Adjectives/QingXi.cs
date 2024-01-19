using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ݴʣ�������
/// </summary>
public class QingXi : AbstractAdjectives
{
    static public string s_description = "+12<sprite name=\"san\">������20s";
    static public string s_wordName = "������";
    public override void Awake()
    {
        adjID = 10;
        wordName = "������";
        bookName = BookNameEnum.CrystalEnergy;
        description = "+12<sprite name=\"san\">������20s";
        skillMode = gameObject.AddComponent<UpPSYMode>();
        skillEffectsTime = 20;
        rarity = 0;
        base.Awake();
    }

    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);
        BasicAbility(aimCharacter);
    }
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {
        aimCharacter.san += 12;
    }

    

    public override void End()
    {
        base.End();
        aim.san -= 12;
    }

}
