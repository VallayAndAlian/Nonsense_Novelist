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
    static public int rarity = 1;
    public override void Awake()
    {
        adjID = 12;
        wordName = "������";
        bookName = BookNameEnum.CrystalEnergy;
        description = "+12<sprite name=\"san\">������20s";
        skillMode = gameObject.AddComponent<UpPSYMode>();
        skillEffectsTime = 20;
        rarity = 1;
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

    protected override void Update()
    {
        base.Update();

    }

    public override void End()
    {
        base.End();
        aim.san -= 12;
    }

}
