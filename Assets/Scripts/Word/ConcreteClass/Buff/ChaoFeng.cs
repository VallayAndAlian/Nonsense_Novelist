using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff£º³°·í
/// </summary>
public class ChaoFeng : AbstractBuff
{
    static public string s_description = "Í£Ö¹ÆÕÍ¨¹¥»÷";
    static public string s_wordName = "³°·í";
    AttackState state;
    override protected void Awake()
    {
        base.Awake();
        buffName = "³°·í";
        description = "Í£Ö¹ÆÕÍ¨¹¥»÷";
        book = BookNameEnum.allBooks;
        isBad = true;
        state=GetComponentInChildren<AttackState>();
    
    }

    public override void Update()
    {
        base.Update();
        state.attackAtime = 0;//ÎÞ·¨Æ½A
    }
    private void OnDestroy()
    {
        base.OnDestroy();   
    }

}


