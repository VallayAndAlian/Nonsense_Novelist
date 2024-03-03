using AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff������
/// </summary>
public class XuRuo : AbstractBuff
{
    static public string s_description = "<sprite name=\"atk\">-30%�����3��";
    static public string s_wordName = "����";
    AttackState state;
    override protected void Awake()
    {
       
        buffName = "����";
        description = "<sprite name=\"atk\">-30%�����3��";
        book = BookNameEnum.allBooks;
        isBad = true;
        isAll = true;
        upup = 3;
        state=GetComponentInChildren<AttackState>();

        base.Awake();

        chara.atkMul -= 0.3f;
    }

    public override void Update()
    {
        base.Update();
      

    }
    private void OnDestroy()
    {
        base.OnDestroy();
        chara.atkMul += 0.3f;
    }

}


