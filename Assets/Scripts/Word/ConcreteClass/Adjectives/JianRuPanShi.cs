using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ݴʣ�������ʯ��
/// </summary>
public class JianRuPanShi : AbstractAdjectives
{
    static public string s_description = "<sprite name=\"def\">+12������10s";
    static public string s_wordName = "������ʯ��";
    public override void Awake()
    {
        adjID = 23;
        wordName = "������ʯ��";
        bookName = BookNameEnum.allBooks;
        description = "<sprite name=\"def\">+12������10s";
        skillMode = gameObject.AddComponent<UpDEFMode>();
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
        aimCharacter.def += 12;
    }

    

    public override void End()
    {
        base.End();
        aim.def -= 12;
    }

}
