using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff������
/// </summary>
public class ChaoFeng : AbstractBuff
{
    static public string s_description = "�������˵Ĺ���";
    static public string s_wordName = "����";
    AttackState state;
    override protected void Awake()
    {
    
        buffName = "����";
        description = "�������˵Ĺ���";
        book = BookNameEnum.allBooks;
        isBad = true;
        state=GetComponentInChildren<AttackState>();
        base.Awake();

    }

    public override void Update()
    {
        base.Update();
        
    }
    private void OnDestroy()
    {
        base.OnDestroy();
       
    }

}


