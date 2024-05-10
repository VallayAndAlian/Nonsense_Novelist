using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ݴʣ�Ұ����
/// </summary>
public class YeSheng : AbstractAdjectives
{
    static public string s_description = "�ٻ�һ�����";
    static public string s_wordName = "Ұ����";
    static public int s_rarity = 1;
    public override void Awake()
    {
        adjID = 27;
        wordName = "Ұ����";
        bookName = BookNameEnum.allBooks;
        description = "�ٻ�һ�����";
        skillMode = gameObject.AddComponent<SelfMode>();
        skillEffectsTime = Mathf.Infinity;
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
        this.GetComponent<AbstractCharacter>().AddRandomServant();
    }

    

    public override void End()
    {
        base.End();
       
    }

}
