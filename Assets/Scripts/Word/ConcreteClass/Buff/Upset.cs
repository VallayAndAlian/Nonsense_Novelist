using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff£º¾ÚÉ¥
/// </summary>
public class Upset : AbstractBuff
{
    static public string s_description = "Í£Ö¹ÆÕÍ¨¹¥»÷";
    static public string s_wordName = "¾ÚÉ¥";
    AttackState state;
    override protected void Awake()
    {
        base.Awake();
        buffName = "¾ÚÉ¥";
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


