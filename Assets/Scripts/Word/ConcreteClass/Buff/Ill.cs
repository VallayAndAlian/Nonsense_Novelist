using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff������
/// </summary>
public class Ill : AbstractBuff
{
    static public string s_description = "ÿ���ܵ�2�������˺�����������10s����<color=#dd7d0e>����</color>,�����޵���";
    static public string s_wordName = "����";

    /// <summary>�ⲿ��ֵʹ����</summary>
    public AbstractCharacter useCharacter;
    float nowTime;
    DamageMode damageMode;
    override protected void Awake()
    {
        
        buffName = "����";
        description = "ÿ���ܵ�2�������˺�����������10s����<color=#dd7d0e>����</color>,�����޵���";
        book = BookNameEnum.FluStudy;
        damageMode = gameObject.AddComponent<DamageMode>();
        damageMode.attackRange=new SingleSelector();
        isBad = true;
        isAll = true;
        nowTime = 0;

        base.Awake();
        StartCoroutine(MakeAttack());
    }


    public override void Update()
    {
        base.Update();

    }

    IEnumerator MakeAttack() 
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            nowTime += 1;
            
             damageMode.UseMode(AttackType.atk, 2,useCharacter != null ? useCharacter : chara, chara,true,0/*2 * (1 - chara.def / (chara.def + 20))*/);
            if (nowTime >= 10)
            {
                var xr=gameObject.AddComponent<XuRuo>();
                xr.maxTime = Mathf.Infinity;
                nowTime = -9999;
            }
        }
    }

    private void OnDestroy()
    {
        base.OnDestroy();
        StopAllCoroutines();
    }
}
