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
       
        buffName = "��ɥ";
        description = "ֹͣ��ͨ����";
        book = BookNameEnum.allBooks;
        isBad = true;
        state=GetComponentInChildren<AttackState>();

        base.Awake();
    }

    public override void Update()
    {
        base.Update();
        chara.charaAnim.SetSpeed(AnimEnum.attack, 0);

    }
    private void OnDestroy()
    {
        base.OnDestroy();
        chara.charaAnim.SetSpeed(AnimEnum.attack, chara.attackSpeedPlus);
    }

}


