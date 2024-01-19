using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ݴʣ���ս��
/// </summary>
public class HaoZhan : AbstractAdjectives
{

    static public string s_description = "���ٹ���������5s";
    static public string s_wordName = "��ս��";
    public override void Awake()
    {                skillEffectsTime = 5;
        base.Awake();
        adjID = 18;
        wordName = "��ս��";
        bookName = BookNameEnum.PHXTwist;
        description = "���ٹ���������5s";

        skillMode = gameObject.AddComponent<SelfMode>();


        rarity = 0;

    }
    bool hasOther = false;
    public override void UseAdj(AbstractCharacter aimCharacter)
    {
        base.UseAdj(aimCharacter);
         foreach(var hz in aimCharacter.GetComponents<HaoZhan>())
        {
            if (hz != this)
            {
                hz.AddTime(skillEffectsTime);
                hasOther = true;
                Destroy(this);
            }
        }
        if (!hasOther)
        { BasicAbility(aimCharacter); }
       
    }

    float record;
    public override void BasicAbility(AbstractCharacter aimCharacter)
    {
        GetComponentInChildren<AI.MyState0>().GetComponent<Animator>().speed = 1.83f;
        record = aimCharacter.attackInterval;
        aimCharacter.attackInterval = record-1.2f;
        if (aimCharacter.attackInterval <= 0.5f) aimCharacter.attackInterval = 0.5f;
    }

    

    public override void End()
    {
        if (hasOther)
        { }
        else
        {
            GetComponentInChildren<AI.MyState0>().GetComponent<Animator>().speed  = 1;
            aim.attackInterval =record;
        }
        base.End();
        Destroy(this);
    }

}
