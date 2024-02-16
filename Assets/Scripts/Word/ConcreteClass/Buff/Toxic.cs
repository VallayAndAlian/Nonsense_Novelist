using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff:�ж�
/// </summary>
public class Toxic : AbstractBuff
{
    static public string s_description = "ÿ���ܵ�5�������˺�";
    static public string s_wordName = "�ж�";
    /// <summary>�ⲿ��ֵʹ����</summary>
    public AbstractCharacter useCharacter;
    float nowTime;
    DamageMode damageMode;
    override protected void Awake()
    {
        base.Awake();
        buffName = "�ж�";
        description = "ÿ���ܵ�5�������˺�";
        book = BookNameEnum.allBooks;
        //
        damageMode = gameObject.AddComponent<DamageMode>();
        damageMode.attackRange=new SingleSelector();
        isBad = true;
    }


    public override void Update()
    {
        base.Update();
        nowTime += Time.deltaTime;
        if(nowTime>1)
        {
            nowTime= 0;
            damageMode.UseMode(AttackType.atk, 5, useCharacter != null ? useCharacter : chara, chara, true, 0);
            
        }
    }
}
