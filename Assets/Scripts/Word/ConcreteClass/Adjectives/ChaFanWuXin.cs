using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ݴʣ��跹���ĵ�
/// </summary>
public class ChaFanWuXin : AbstractAdjectives
{
    static public string s_description = "��10s�� <sprite name=\"psy\">+8���޷�����";
    static public string s_wordName = "�跹���ĵ�";


    public override void Awake()
    {
        
        adjID = 1;
        wordName = "�跹���ĵ�";
        bookName = BookNameEnum.HongLouMeng;
        description = "��10s�� <sprite name=\"psy\">+8���޷�����";

        skillMode = gameObject.AddComponent<UpPSYMode>();
        skillEffectsTime = 10;

        rarity = 1;
        base.Awake();
    }

    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);

        buffs.Add(aimCharacter.gameObject.AddComponent<Upset>());
        buffs[0].maxTime = skillEffectsTime;

        BasicAbility(aimCharacter);
    }
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {
        aimCharacter.psy += 8;
    }

    public override void End()
    {
        base.End();
        aim.psy -= 8;
    }
}
