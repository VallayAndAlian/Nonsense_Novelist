using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff������
/// </summary>
public class Ill : AbstractBuff
{
    static public string s_description = "ÿ���ܵ�2�������˺�����������30s����<color=#dd7d0e>����</color>";
    static public string s_wordName = "����";

    /// <summary>�ⲿ��ֵʹ����</summary>
    public AbstractCharacter useCharacter;
    float nowTime;
    DamageMode damageMode;
    override protected void Awake()
    {
        base.Awake();
        buffName = "����";
        description = "ÿ���ܵ�2�������˺�����������30s����<color=#dd7d0e>����</color>";
        book = BookNameEnum.FluStudy;
        damageMode = gameObject.AddComponent<DamageMode>();
        damageMode.attackRange=new SingleSelector();
        isBad = true;
        isAll = true;
        nowTime = 0;

        StartCoroutine(MakeAttack());
    }


    public override void Update()
    {
        base.Update();
        //nowTime += Time.deltaTime;
        //if(nowTime>1)
        //{
        //    nowTime= 0;
        //    damageMode.UseMode(useCharacter!=null?useCharacter:chara, 2 * (1 - chara.def / (chara.san + 20)), chara);
        //}
    }

    IEnumerator MakeAttack() 
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            nowTime += 1;
            
             damageMode.UseMode(useCharacter != null ? useCharacter : chara, 2 * (1 - chara.def / (chara.def + 20)), chara);
            if (nowTime >= 30)
            {
                //buffs.Add(gameObject.AddComponent<>());
                //buffs[0].maxTime = Mathf.Infinity;
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
