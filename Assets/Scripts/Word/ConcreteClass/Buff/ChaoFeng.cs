using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff������
/// </summary>
public class ChaoFeng : AbstractBuff
{
    static public string s_description = "ֹͣ��ͨ����";
    static public string s_wordName = "����";
    AttackState state;
    override protected void Awake()
    {
        base.Awake();
        buffName = "����";
        description = "ֹͣ��ͨ����";
        book = BookNameEnum.allBooks;
        isBad = true;
        state=GetComponentInChildren<AttackState>();
    
    }

    public override void Update()
    {
        base.Update();
        state.attackAtime = 0;//�޷�ƽA
    }
    private void OnDestroy()
    {
        base.OnDestroy();   
    }

}


