using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff����ɥ
/// </summary>
public class Upset : AbstractBuff
{
    static public string s_description = "ֹͣ��ͨ����";
    static public string s_wordName = "��ɥ";
    AttackState state;
    override protected void Awake()
    {
        base.Awake();
        buffName = "��ɥ";
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


