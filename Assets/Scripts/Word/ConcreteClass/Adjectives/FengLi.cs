using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ݴʣ�������
/// </summary>
public class FengLi : AbstractAdjectives
{
    static public string s_description = "<sprite name=\"atk\">+4������10s";
    static public string s_wordName = "������";
    static public int rarity = 1;

    public override void Awake()
    {
        adjID = 21;
        wordName = "������";
        bookName = BookNameEnum.allBooks;
        description = "<sprite name=\"atk\">+4������10s";

        skillMode = gameObject.AddComponent<UpATKMode>();

        skillEffectsTime = 10;
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
        aimCharacter.atk += 4;
    }

    

    public override void End()
    {
        base.End();
        aim.atk -=4;
    }

}
